using Flunt.Notifications;

namespace Application.Domain.Core.Domain
{
    public class ResponseResult
    {
        private IList<Notification> _failMessages { get; }

        public IReadOnlyCollection<Notification> Fails => _failMessages.ToList();

        public bool HasFails => _failMessages.Any();

        public object? Data { get; private set; }

        /// <summary>
        /// Cria um novo objeto de retorno para a api
        /// </summary>
        public ResponseResult()
        {
            _failMessages = new List<Notification>();
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="object">Objeto que deverá ser serializado pela Api</param>
        public ResponseResult(object @object) : this()
        {
            AddValue(@object);
        }

        /// <summary>
        /// Adiciona um objeto que deverá ser serializado e retornado pela Api
        /// </summary>
        /// <param name="object">Objeto que deverá ser serializado pela Api</param>
        public void AddValue(object @object)
        {
            Data = @object;
        }

        /// <summary>
        /// Adiciona mensagem de retorno
        /// </summary>
        /// <param name="notification">Mensagem que deverá ser retornada pela Api</param>
        public void AddNotification(Notification notification)
        {
            _failMessages.Add(notification);
        }

        /// <summary>
        /// Adiciona mensagens de retorno
        /// </summary>
        /// <param name="notifications">Notificações</param>
        /// <param name="type">Tipo de notificação</param>
        public void AddNotifications(IEnumerable<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                AddNotification(notification);
            }
        }

        public override string ToString()
        {
            return string.Join(" - ", Fails.Select(x => x.Message));
        }
    }
}
