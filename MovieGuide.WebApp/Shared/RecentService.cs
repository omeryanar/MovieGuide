using System.Collections;
using Blazored.LocalStorage;
using MovieGuide.Common.Model.Search;

namespace MovieGuide.WebApp.Shared
{
    public class RecentService
    {
        public RecentService(ILocalStorageService localStorage)
        {
            LocalStorage = localStorage;
        }

        public async Task<List<SearchBase>> GetRecentItems()
        {
            if (RecentItems == null)
                await Initialize();

            return RecentItems.ToList();
        }

        public async Task AddRecentItem(SearchBase item)
        {
            if (RecentItems == null)
                await Initialize();

            RecentItems.Add(item);
            await LocalStorage.SetItemAsync(LocalStorageKey, RecentItems.AsEnumerable());
        }

        public async Task RemoveRecentItem(SearchBase item)
        {
            if (RecentItems == null)
                await Initialize();

            RecentItems.Remove(item);
            await LocalStorage.SetItemAsync(LocalStorageKey, RecentItems.AsEnumerable());
        }

        private async Task Initialize()
        {
            IEnumerable<SearchBase> items = await LocalStorage.GetItemAsync<IEnumerable<SearchBase>>(LocalStorageKey);
            if (items == null)
                RecentItems = new RecentItems<SearchBase>(60);
            else
                RecentItems = new RecentItems<SearchBase>(60, items);
        }

        private const string LocalStorageKey = "RecentItems";

        private static RecentItems<SearchBase> RecentItems;

        private static ILocalStorageService LocalStorage;
    }

    public class RecentItems<T> : IEnumerable<T> where T : SearchBase
    {
        public IEnumerator<T> GetEnumerator() => linkedList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => linkedList.GetEnumerator();

        public int Capacity { get; private set; }

        public RecentItems(int capacity)
        {
            Capacity = capacity;
        }

        public RecentItems(int capacity, IEnumerable<T> items)
        {
            Capacity = capacity;

            int count = 0;
            foreach (T item in items)
            {
                string key = $"{item.MediaType}_{item.Id}";
                LinkedListNode<T> node = new LinkedListNode<T>(item);

                dictionary[key] = node;
                linkedList.AddLast(node);

                count++;
                if (count == capacity)
                    break;
            }
        }

        public void Add(T item)
        {
            string key = $"{item.MediaType}_{item.Id}";

            if (dictionary.ContainsKey(key))
                linkedList.Remove(dictionary[key]);

            LinkedListNode<T> node = new LinkedListNode<T> (item);

            dictionary[key] = node;
            linkedList.AddFirst(node);

            if (linkedList.Count > Capacity)
                linkedList.RemoveLast();
        }

        public void Remove(T item)
        {
            string key = $"{item.MediaType}_{item.Id}";

            if (!dictionary.ContainsKey(key))
                return;

            linkedList.Remove(dictionary[key]);
            dictionary.Remove(key);
        }

        private LinkedList<T> linkedList = new LinkedList<T>();

        private Dictionary<string, LinkedListNode<T>> dictionary = new Dictionary<string, LinkedListNode<T>>();
    }
}
