namespace PropertyTracker.Application.Services.Converters
{
    public static class SearchConverter
    {
        public static DTO.Search ConvertToDTO(this Domain.Model.Search model)
        {
            return new DTO.Search()
            {
                Id = model.Id,
                SiteId = model.SiteId,
                Url = model.Url
            };
        }

        public static Domain.Model.Search ConvertToModel(this DTO.Search dto)
        {
            return new Domain.Model.Search()
            {
                Id = dto.Id,
                SiteId = dto.SiteId,
                Url = dto.Url
            };
        }
    }
}
