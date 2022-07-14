
using AutoMapper;
using WebApp.API.DTOs.Attackments;
using WebApp.API.DTOs.Boards;
using WebApp.API.DTOs.Labels;
using WebApp.API.DTOs.ListTasks;
using WebApp.API.DTOs.Tasks;
using WebApp.API.DTOs.ToDos;
using WebApp.API.DTOs.Users;
using WebApp.Domain.Boards;
using WebApp.Domain.Labels;
using WebApp.Domain.ListTasks;
using WebApp.Domain.Tasks;
using WebApp.Domain.ToDos;
using WebApp.Domain.Users;

namespace ToDoApp.Service
{
    public class ApplicationAutoMapperProfile : Profile
    {
        public ApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<AddBoard, Board>();
            CreateMap<Board, BoardResponse>();
            CreateMap<ListTask, ListTaskReponseGetBoard>();
            CreateMap<User, UserReponse>();
            CreateMap<User, AddUserResponse>();
            CreateMap<AddUserRequest, User>();
            CreateMap<Board, BoardGetUserReponse>();
            CreateMap<AddBoardMemberRequest, BoardMember>();
            CreateMap<AddListTaskRequest, ListTask>();
            CreateMap<UpdateListTaskRequest, ListTask>();
            CreateMap<ListTask, ListTaskResponse>();
            CreateMap<Task, TaskResponseGetListTask>();
            CreateMap<ToDo, ToDoResponse>();
            CreateMap<AddTaskRequest, Task>();
            CreateMap<UpdateTaskRequest, Task>();
            CreateMap<Task, GetTaskResponse>();
            CreateMap<Label, LabelResponse>();
            CreateMap<Attackment, AttackmentResponse>();
            CreateMap<AddTaskMemberRequest, TaskMember>();
            CreateMap<AddToDoRequest, ToDo>();
            CreateMap<AddTaskLabelRequest, TaskLabel>();
            CreateMap<AddAttackmentRequest, Attackment>();
            CreateMap<UpdateBoardRequest, Board>();
            CreateMap<UpdateListTaskRequest, ListTask>();
            CreateMap<UpdateTaskRequest, Task>();
            CreateMap<UpdateUserRequest, User>();
        }
    }
}
