using AutoMapper;
using Gym.Api.DTOs.Request;
using Gym.Api.DTOs.Response;
using Gym.Domain.Entities;

namespace Gym.Api.Mappings
{
    public class GymMappingProfile : Profile
    {
        public GymMappingProfile()
        {
            // Member mappings
            CreateMap<Member, MemberResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.MembershipType, opt => opt.MapFrom(src => src.MembershipType.ToString()));

            CreateMap<CreateMemberRequest, Member>();
            CreateMap<UpdateMemberRequest, Member>();

            // Trainer mappings
            CreateMap<Trainer, TrainerResponse>()
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialization.ToString()));

            CreateMap<CreateTrainerRequest, Trainer>();
            CreateMap<UpdateTrainerRequest, Trainer>();

            // GymClass mappings
            CreateMap<GymClassRequestDto, GymClass>();
            CreateMap<GymClass, GymClassResponseDto>();

            // Enrollment mappings
            CreateMap<Enrollment, EnrollmentResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<CreateEnrollmentRequest, Enrollment>();

            // Membership mappings
            CreateMap<Membership, MembershipResponse>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
            CreateMap<CreateMembershipRequest, Membership>();
        }
    }
}
