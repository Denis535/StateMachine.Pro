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

    private protected abstract IStateful<TThis>? Owner { get; set; }
    public abstract IStateful<TThis>? Stateful { get; }

    public abstract event Action<object?>? OnBeforeAttachEvent;
    public abstract event Action<object?>? OnAfterAttachEvent;
    public abstract event Action<object?>? OnBeforeDetachEvent;
    public abstract event Action<object?>? OnAfterDetachEvent;

    public StateBase() {
    }

    internal abstract void Attach(IStateful<TThis> owner, object? argument);
    internal abstract void Detach(IStateful<TThis> owner, object? argument);

    protected abstract void OnAttach(object? argument);
    protected abstract void OnBeforeAttach(object? argument);
    protected abstract void OnAfterAttach(object? argument);

    protected abstract void OnDetach(object? argument);
    protected abstract void OnBeforeDetach(object? argument);
    protected abstract void OnAfterDetach(object? argument);

}
public abstract partial class StateBase<TThis> where TThis : StateBase<TThis> {
    public enum Activity_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    public abstract Activity_ Activity { get; private protected set; }

    public abstract event Action<object?>? OnBeforeActivateEvent;
    public abstract event Action<object?>? OnAfterActivateEvent;
    public abstract event Action<object?>? OnBeforeDeactivateEvent;
    public abstract event Action<object?>? OnAfterDeactivateEvent;

    internal abstract void Activate(object? argument);
    internal abstract void Deactivate(object? argument);

    protected abstract void OnActivate(object? argument);
    protected abstract void OnBeforeActivate(object? argument);
    protected abstract void OnAfterActivate(object? argument);

    protected abstract void OnDeactivate(object? argument);
    protected abstract void OnBeforeDeactivate(object? argument);
    protected abstract void OnAfterDeactivate(object? argument);

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

    private protected abstract object? Owner { get; set; }
    public abstract IStateful<TThis>? Stateful { get; }

    public abstract event Action<object?>? OnBeforeAttachEvent;
    public abstract event Action<object?>? OnAfterAttachEvent;
    public abstract event Action<object?>? OnBeforeDetachEvent;
    public abstract event Action<object?>? OnAfterDetachEvent;

    public StateBase() {
    }

    internal abstract void Attach(IStateful<TThis> owner, object? argument);
    internal abstract void Detach(IStateful<TThis> owner, object? argument);

    internal abstract void Attach(TThis owner, object? argument);
    internal abstract void Detach(TThis owner, object? argument);

    protected abstract void OnAttach(object? argument);
    protected abstract void OnBeforeAttach(object? argument);
    protected abstract void OnAfterAttach(object? argument);

    protected abstract void OnDetach(object? argument);
    protected abstract void OnBeforeDetach(object? argument);
    protected abstract void OnAfterDetach(object? argument);

}
public abstract partial class StateBase<TThis> {
    public enum Activity_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    public abstract Activity_ Activity { get; private protected set; }

    public abstract event Action<object?>? OnBeforeActivateEvent;
    public abstract event Action<object?>? OnAfterActivateEvent;
    public abstract event Action<object?>? OnBeforeDeactivateEvent;
    public abstract event Action<object?>? OnAfterDeactivateEvent;

    internal abstract void Activate(object? argument);
    internal abstract void Deactivate(object? argument);

    protected abstract void OnActivate(object? argument);
    protected abstract void OnBeforeActivate(object? argument);
    protected abstract void OnAfterActivate(object? argument);

    protected abstract void OnDeactivate(object? argument);
    protected abstract void OnBeforeDeactivate(object? argument);
    protected abstract void OnAfterDeactivate(object? argument);

}
public abstract partial class StateBase<TThis> {

    [MemberNotNullWhen( false, nameof( Parent ) )] public abstract bool IsRoot { get; }
    public abstract TThis Root { get; }

    public abstract TThis? Parent { get; }
    public abstract IEnumerable<TThis> Ancestors { get; }
    public abstract IEnumerable<TThis> AncestorsAndSelf { get; }

    public abstract TThis? Child { get; private protected set; }
    public abstract IEnumerable<TThis> Descendants { get; }
    public abstract IEnumerable<TThis> DescendantsAndSelf { get; }

    protected abstract void SetChild(TThis? child, object? argument, Action<TThis>? callback);
    protected abstract void AddChild(TThis child, object? argument);
    protected abstract void RemoveChild(TThis child, object? argument, Action<TThis>? callback);
    protected abstract void RemoveSelf(object? argument, Action<TThis>? callback);

}
```
