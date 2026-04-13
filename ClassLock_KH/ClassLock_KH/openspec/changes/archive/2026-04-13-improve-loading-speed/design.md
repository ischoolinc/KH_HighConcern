## Context

The application acts as a FISCA plugin and monitors various student and class events to update its internal state (`_UDT_ClassLockDict` and `_ClassStudentDict`) and refresh UI columns. Currently, every event trigger causes an immediate and full background reload of all data, which is inefficient during batch operations or rapid user interactions.

## Goals / Non-Goals

**Goals:**
- Implement a 500ms debounce for data reload events to consolidate multiple rapid triggers into a single reload.
- Prevent redundant UDT schema synchronization calls.
- Improve perceived performance and reduce database overhead.

**Non-Goals:**
- Changing the underlying data structures or UDT schemas.
- Implementing partial data updates (staying with full refresh for simplicity but reducing frequency).
- Modifying the external `KH_HighConcernCalc` library.

## Decisions

### 1. Debouncing mechanism using `System.Windows.Forms.Timer`
- **Choice**: Use a static `Timer` in `Program.cs` to manage reloads.
- **Rationale**: The application is WinForms-based. A `Timer` provides a simple way to wait for a "quiet period" before triggering the background worker. It also ensures the trigger happens on the UI thread, which is safe for checking `BackgroundWorker.IsBusy`.
- **Alternative**: `System.Threading.Timer` or `Task.Delay`. These would require more complex thread marshalling and state management.

### 2. Guard for `SyncSchema`
- **Choice**: Introduce a static boolean flag `_isSchemaSynced` in `UDTTransfer`.
- **Rationale**: `SyncSchema` is an expensive operation that checks table structures. It only needs to run once per application session.
- **Alternative**: Move `CreateUDTTable` to a module initializer, but the current `UDTTransfer` static method is more flexible for the existing code structure.

### 3. Consolidating event handlers
- **Choice**: Redirect all `FISCA.InteractionService.SubscribeEvent` handlers to a single `TriggerReload()` method.
- **Rationale**: Centralizing the trigger logic makes it easier to manage the debounce timer and any future optimization logic.

## Risks / Trade-offs

- **[Risk] Debounce Delay**  A 500ms delay might be perceived by the user if they expect immediate updates. However, most FISCA operations involve batching, so this is generally acceptable.
- **[Risk] BackgroundWorker Conflict**  If a reload is triggered while one is already running, the debounce timer will wait, but we must ensure we don't lose the "last" update request.
- **Mitigation**: The `Timer` will keep restarting as long as events arrive. If it ticks and `_bgLLoadUDT.IsBusy`, we can set a flag to reload again once the current one completes.
