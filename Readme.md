# Overview
The library that helps you implement a stateful object, i.e. state pattern.

# Reference
```
public interface IStateful<T> where T : StateBase<T> {

    protected T? State { get; }

    protected internal void SetState(T? state, object? argument = null);

}
public abstract class StateBase<TThis> where TThis : StateBase<TThis> {
    public enum Activity_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    private IStateful<TThis>? Owner { get; set; }
    public Activity_ Activity { get; private set; }

    public IStateful<TThis>? Stateful { get; }

    public event Action<object?>? OnBeforeActivateEvent;
    public event Action<object?>? OnAfterActivateEvent;
    public event Action<object?>? OnBeforeDeactivateEvent;
    public event Action<object?>? OnAfterDeactivateEvent;

    public StateBase();

    internal void Attach(IStateful<TThis> owner, object? argument);
    internal void Detach(IStateful<TThis> owner, object? argument);

    private void Activate(object? argument);
    private void Deactivate(object? argument);

    private void OnBeforeActivateInternal(object? argument);
    private void OnAfterActivateInternal(object? argument);
    private void OnBeforeDeactivateInternal(object? argument);
    private void OnAfterDeactivateInternal(object? argument);

    protected virtual void OnBeforeActivate(object? argument);
    protected abstract void OnActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
```
