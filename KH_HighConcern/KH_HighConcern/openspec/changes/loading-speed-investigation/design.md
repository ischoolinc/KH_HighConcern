## Context

The plugin currently initializes by fetching all records from a UDT table synchronously on the UI thread. This causes the main application to freeze during startup. Similarly, importing data and sending logs to the server involve synchronous operations that degrade performance.

## Goals / Non-Goals

**Goals:**
- Eliminate UI blocking during plugin initialization in `Program.Main`.
- Improve responsiveness of the student list by loading high-concern data in the background.
- Prevent UI hangs during data synchronization with the server in `Utility`.
- Reduce initial wait time when opening the import wizard.

**Non-Goals:**
- Rewriting the entire application architecture.
- Changing the underlying data storage (UDT).
- Optimizing server-side performance (out of scope for this client-side investigation).

## Decisions

- **BackgroundWorker for Main Initialization**: Wrap the `UDTTransfer.GetHighConcernDictAll()` call in a `BackgroundWorker`. This aligns with existing patterns in the codebase and is well-supported in .NET 4.8.
- **Lazy Loading or Async Refresh for List Fields**: `ListPaneField`s will initially display empty or placeholder values and will be refreshed once the background data load completes.
- **Async HTTP Calls in Utility**: Convert `HttpWebRequest` calls in `Utility.cs` to use `BeginGetResponse` or wrap them in tasks to ensure they don't block the calling thread.
- **Targeted Data Loading in Import**: Modify `ImportHighConcern.Prepare` to only fetch student records if they are not already cached or to fetch only a subset if possible. However, since the wizard needs to validate 學號 (Student Number), we will optimize the query to fetch only the mapping of Student Number to ID instead of full student records initially.

## Risks / Trade-offs

- [Risk] Race conditions when updating `_HighConcernDict`.
  - [Mitigation] Use thread-safe access patterns or ensure updates happen on the UI thread via `RunWorkerCompleted` or `Control.Invoke`.
- [Risk] Inconsistent data display while loading.
  - [Mitigation] Provide visual cues (e.g., placeholder text) if data is not yet available in the list view.
- [Risk] Breaking existing framework assumptions about synchronous initialization.
  - [Mitigation] Ensure `AddListPaneField` and other UI registrations still happen synchronously, but the *data* they use is updated later.
