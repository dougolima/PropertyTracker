namespace PropertyTracker.Application.Services.Converters
{
    public static class PropertyConverter
    {
        public static DTO.Property ConvertToDTO(this Domain.Model.Property model)
        {
            return new DTO.Property()
            {
                Id = model.Id,
                Deleted = model.Deleted,
                DeletedDate = model.DeletedDate,
                ExternalId = model.ExternalId,
                Photo = model.Photo,
                SearchId = model.SearchId,
                SiteId = model.SiteId,
                Title = model.Title,
                Url = model.Url,
                Value = model.Value,
                Visited = model.Visited
            };
        }

        public static Domain.Model.Property ConvertToModel(this DTO.Property dto)
        {
            return new Domain.Model.Property()
            {
                Id = dto.Id,
                Deleted = dto.Deleted,
                DeletedDate = dto.DeletedDate,
                ExternalId = dto.ExternalId,
                Photo = dto.Photo,
                SearchId = dto.SearchId,
                SiteId = dto.SearchId,
                Title = dto.Title,
                Url = dto.Url,
                Value = dto.Value,
                Visited = dto.Visited
            };
        }
    }
}
