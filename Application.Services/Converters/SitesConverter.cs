namespace PropertyTracker.Application.Services.Converters
{
    public static class SiteConverter
    {
        public static DTO.Site ConvertToDTO(this Domain.Model.Site model)
        {
            return new DTO.Site()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static Domain.Model.Site ConvertToDTO(this DTO.Site dto)
        {
            return new Domain.Model.Site()
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}
