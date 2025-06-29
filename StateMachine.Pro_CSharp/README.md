# Overview
The library that allows you to easily implement a stateful object.

# Reference
```
namespace System.StateMachine;
public interface IStateful<T> where T : notnull, StateBase<T> {

    protected T? State { get; set; }

    protected void SetState(T? state, object? argument, Action<T, object?>? callback);
    protected void AddState(T state, object? argument);
    protected void RemoveState(T state, object? argument, Action<T, object?>? callback);
    protected void RemoveState(object? argument, Action<T, object?>? callback);

}
public abstract partial class StateBase<TThis> where TThis : notnull, StateBase<TThis> {
    public enum Activity_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    public IStateful<TThis>? Stateful { get; }

    public Activity_ Activity { get; }

    public StateBase();

}
public abstract partial class StateBase<TThis> {

    public event Action<object?>? OnBeforeAttachCallback;
    public event Action<object?>? OnAfterAttachCallback;
    public event Action<object?>? OnBeforeDetachCallback;
    public event Action<object?>? OnAfterDetachCallback;

    protected abstract void OnAttach(object? argument);
    protected virtual void OnBeforeAttach(object? argument);
    protected virtual void OnAfterAttach(object? argument);

    protected abstract void OnDetach(object? argument);
    protected virtual void OnBeforeDetach(object? argument);
    protected virtual void OnAfterDetach(object? argument);

}
public abstract partial class StateBase<TThis> {

    public event Action<object?>? OnBeforeActivateCallback;
    public event Action<object?>? OnAfterActivateCallback;
    public event Action<object?>? OnBeforeDeactivateCallback;
    public event Action<object?>? OnAfterDeactivateCallback;

    protected abstract void OnActivate(object? argument);
    protected virtual void OnBeforeActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);

    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
```

# Reference (Hierarchical)
```
namespace System.StateMachine.Hierarchical;
public interface IStateful<T> where T : notnull, StateBase<T> {

    protected T? State { get; set; }

    protected void SetState(T? state, object? argument, Action<T, object?>? callback);
    protected void AddState(T state, object? argument);
    protected void RemoveState(T state, object? argument, Action<T, object?>? callback);
    protected void RemoveState(object? argument, Action<T, object?>? callback);

}
public abstract partial class StateBase<TThis> where TThis : notnull, StateBase<TThis> {
    public enum Activity_ {
        Inactive,
        Activating,
        Active,
        Deactivating,
    }

    public IStateful<TThis>? Stateful { get; }

    public bool IsRoot { get; }
    public TThis Root { get; }

    public TThis? Parent { get; }
    public IEnumerable<TThis> Ancestors { get; }
    public IEnumerable<TThis> AncestorsAndSelf { get; }

    public Activity_ Activity { get; }

    public TThis? Child { get; }
    public IEnumerable<TThis> Descendants { get; }
    public IEnumerable<TThis> DescendantsAndSelf { get; }

    public StateBase();

}
public abstract partial class StateBase<TThis> {

    public event Action<object?>? OnBeforeAttachCallback;
    public event Action<object?>? OnAfterAttachCallback;
    public event Action<object?>? OnBeforeDetachCallback;
    public event Action<object?>? OnAfterDetachCallback;

    protected abstract void OnAttach(object? argument);
    protected virtual void OnBeforeAttach(object? argument);
    protected virtual void OnAfterAttach(object? argument);

    protected abstract void OnDetach(object? argument);
    protected virtual void OnBeforeDetach(object? argument);
    protected virtual void OnAfterDetach(object? argument);

}
public abstract partial class StateBase<TThis> {

    public event Action<object?>? OnBeforeActivateCallback;
    public event Action<object?>? OnAfterActivateCallback;
    public event Action<object?>? OnBeforeDeactivateCallback;
    public event Action<object?>? OnAfterDeactivateCallback;

    protected abstract void OnActivate(object? argument);
    protected virtual void OnBeforeActivate(object? argument);
    protected virtual void OnAfterActivate(object? argument);

    protected abstract void OnDeactivate(object? argument);
    protected virtual void OnBeforeDeactivate(object? argument);
    protected virtual void OnAfterDeactivate(object? argument);

}
public abstract partial class StateBase<TThis> {

    protected void SetChild(TThis? child, object? argument, Action<TThis, object?>? callback);
    protected virtual void AddChild(TThis child, object? argument);
    protected virtual void RemoveChild(TThis child, object? argument, Action<TThis, object?>? callback);
    protected void RemoveChild(object? argument, Action<TThis, object?>? callback);
    protected void RemoveSelf(object? argument, Action<TThis, object?>? callback);

}
```

# Link
- https://github.com/Denis535/StateMachine.Pro
- https://www.nuget.org/packages/StateMachine.Pro
