using Application.Domain.Core.Domain;

namespace Application.Domain.Core.Validator
{
    public class ValidatorResponse
    {
        protected readonly ResponseResult _response;

        public ValidatorResponse()
        {
            _response = new ResponseResult();
        }
    }
}
