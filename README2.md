# Overview
The library that allows you to easily implement a stateful object.

# Reference
```
namespace System.StateMachine;
public interface IStateful<T> where T : StateBase<T> {

    protected T? State { get; set; }

    protected void SetState(T? state, object? argument, Action<T>? callback);
    protected void AddState(T state, object? argument);
    protected void RemoveState(T state, object? argument, Action<T>? callback);

}
public abstract partial class StateBase<TThis> where TThis : StateBase<TThis> {

    private IStateful<TThis>? Owner { get; set; }
    public IStateful<TThis>? Stateful { get; }

    public event Action<object?>? OnBeforeAttachEvent;
    public event Action<object?>? OnAfterAttachEvent;
    public event Action<object?>? OnBeforeDetachEvent;
    public event Action<object?>? OnAfterDetachEvent;

    public StateBase();

    private void AttachBase(IStateful<TThis> owner, object? argument);
    private void DetachBase(IStateful<TThis> owner, object? argument);

    protected abstract void OnAttach(object? argument);
    protected virtual void OnBeforeAttach(object? argument);
    protected virtual void OnAfterAttach(object? argument);

    protected abstract void OnDetach(object? argument);
    protected virtual void OnBeforeDetach(object? argument);
    protected virtual void OnAfterDetach(object? argument);

}
public abstract partial class StateBase<TThis> {
    public enum Activity_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    public Activity_ Activity { get; private set; }

    public event Action<object?>? OnBeforeActivateEvent;
    public event Action<object?>? OnAfterActivateEvent;
    public event Action<object?>? OnBeforeDeactivateEvent;
    public event Action<object?>? OnAfterDeactivateEvent;

    //public StateBase();

    internal void Attach(IStateful<TThis> owner, object? argument);
    internal void Detach(IStateful<TThis> owner, object? argument);

    private void Activate(object? argument);
    private void Deactivate(object? argument);

    protected abstract void OnActivate(object? argument);
    protected virtual void OnBeforeActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);

    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
```
```
namespace System.StateMachine.Hierarchical;
public interface IStateful<T> where T : StateBase<T> {

    protected T? State { get; set; }

    protected void SetState(T? state, object? argument, Action<T>? callback);
    protected void AddState(T state, object? argument);
    protected internal void RemoveState(T state, object? argument, Action<T>? callback);

}
public abstract partial class StateBase<TThis> where TThis : StateBase<TThis> {

    private object? Owner { get; set; }
    public IStateful<TThis>? Stateful { get; }

    public event Action<object?>? OnBeforeAttachEvent;
    public event Action<object?>? OnAfterAttachEvent;
    public event Action<object?>? OnBeforeDetachEvent;
    public event Action<object?>? OnAfterDetachEvent;

    public StateBase();

    private void AttachBase(IStateful<TThis> owner, object? argument);
    private void DetachBase(IStateful<TThis> owner, object? argument);

    private void AttachBase(TThis owner, object? argument);
    private void DetachBase(TThis owner, object? argument);

    protected abstract void OnAttach(object? argument);
    protected virtual void OnBeforeAttach(object? argument);
    protected virtual void OnAfterAttach(object? argument);

    protected abstract void OnDetach(object? argument);
    protected virtual void OnBeforeDetach(object? argument);
    protected virtual void OnAfterDetach(object? argument);

}
public abstract partial class StateBase<TThis> {

    [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
    public TThis Root { get; }

    public TThis? Parent { get; }
    public IEnumerable<TThis> Ancestors { get; }
    public IEnumerable<TThis> AncestorsAndSelf { get; }

    public TThis? Child { get; }
    public IEnumerable<TThis> Descendants { get; }
    public IEnumerable<TThis> DescendantsAndSelf { get; }

    //public StateBase();

    protected void SetChild(TThis? child, object? argument, Action<TThis>? callback);
    protected void AddChild(TThis child, object? argument);
    protected void RemoveChild(TThis child, object? argument, Action<TThis>? callback);
    protected void RemoveSelf(object? argument, Action<TThis>? callback);

}
public abstract partial class StateBase<TThis> {
    public enum Activity_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    public Activity_ Activity { get; private set; }

    public event Action<object?>? OnBeforeActivateEvent;
    public event Action<object?>? OnAfterActivateEvent;
    public event Action<object?>? OnBeforeDeactivateEvent;
    public event Action<object?>? OnAfterDeactivateEvent;

    //public StateBase();

    internal void Attach(IStateful<TThis> owner, object? argument);
    internal void Detach(IStateful<TThis> owner, object? argument);

    private void Activate(object? argument);
    private void Deactivate(object? argument);

    protected abstract void OnActivate(object? argument);
    protected virtual void OnBeforeActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);

    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
```
