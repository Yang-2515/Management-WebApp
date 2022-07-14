using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.API.DTOs.Histories;
using WebApp.Domain.Histories;
using WebApp.Domain.Interfaces;

namespace WebApp.API.Services.Histories
{
    public class HistoryService: BaseService
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly IMapper _mapper;
        public HistoryService(IUnitOfWork unitOfWork,
            IHistoryRepository historyRepository,
            IMapper mapper): base(unitOfWork)
        {
            _historyRepository = historyRepository;
            _mapper = mapper;
        }
        public async Task AddHistoryAsync(AddHistoryRequest addHistoryRequest, int userId)
        {
            History history = _mapper.Map<AddHistoryRequest, History>(addHistoryRequest);
            //history.AddInformation(userId);
            await _historyRepository.AddAsync(history);
        }
    }
}
