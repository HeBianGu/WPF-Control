namespace H.Services.Common
{
    public interface ITransitionHostable
    {
        public ITransitionable Transitionable { get; set; }
    }
}
