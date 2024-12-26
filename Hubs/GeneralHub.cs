using Microsoft.AspNetCore.SignalR;
using AracKiralama.Repositories;

namespace AracKiralama.Hubs
{
    public class GeneralHub : Hub
    {
        private readonly CategoryRepository _categoryRepository;

        public GeneralHub(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task SendCategoryCount()
        {
            // Aktif kategori sayısını hesaplar
            int categoryCount = _categoryRepository.Where(c => c.IsActive).Count();

            // Tüm istemcilere kategori sayısını gönderir
            await Clients.All.SendAsync("onCategoryAdd", categoryCount);
        }

        public async Task SendCategoryUpdate()
        {
            // Güncel kategori sayısını hesaplar
            int categoryCount = _categoryRepository.Where(c => c.IsActive).Count();

            // Tüm istemcilere kategori güncellemesini gönderir
            await Clients.All.SendAsync("onCategoryUpdate", categoryCount);
        }

        public async Task SendCategoryDelete()
        {
            // Güncel kategori sayısını hesaplar
            int categoryCount = _categoryRepository.Where(c => c.IsActive).Count();

            // Tüm istemcilere kategori silme bildirimini gönderir
            await Clients.All.SendAsync("onCategoryDelete", categoryCount);
        }
    }
}
