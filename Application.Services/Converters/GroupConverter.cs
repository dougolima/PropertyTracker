namespace PropertyTracker.Application.Services.Converters
{
    public static class GroupConverter
    {
        public static DTO.Group ConvertToDTO(this Domain.Model.Group model)
        {
            return new DTO.Group()
            {
                Id = model.Id,
                Name = model.Name,
                Active = model.Active,
                Description = model.Description,
                UserId = model.UserId
            };
        }

        public static Domain.Model.Group ConvertToModel(this DTO.Group dto)
        {
            return new Domain.Model.Group()
            {
                Id = dto.Id,
                Name = dto.Name,
                Active = dto.Active,
                Description = dto.Description,
                UserId = dto.UserId
            };
        }
    }
}
