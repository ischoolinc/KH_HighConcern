## Why

The application currently suffers from slow loading times during startup and certain operations because heavy data fetching and network requests are performed synchronously on the main UI thread. This leads to a poor user experience where the application appears frozen or unresponsive during initialization and data updates.

## What Changes

- **Asynchronous Data Loading in Program.Main**: Refactor the initial data fetch of high-concern student records to be asynchronous, preventing the UI thread from blocking during plugin initialization.
- **Asynchronous Event Handling**: Modify the subscription to data update events to perform re-fetching in the background.
- **Optimized Data Fetching in Import**: Refactor the import preparation process to load only necessary data or load it more efficiently, reducing the initial delay when opening the import wizard.
- **Asynchronous Network Requests in Utility**: Update `Utility.SendData` and `Utility.SendDataList` to perform HTTP requests asynchronously to avoid UI hangs during data synchronization with the server.

## Capabilities

### New Capabilities
- `async-data-initialization`: Move all synchronous data fetching from `Program.Main` to background tasks with proper UI synchronization.
- `async-utility-requests`: Implement asynchronous versions of network-dependent methods in the `Utility` class.
- `efficient-import-preparation`: Optimize the data loading strategy for the import wizard to handle large student datasets without blocking the UI.

### Modified Capabilities
(None)

## Impact

- `Program.cs`: Significant refactoring of the `Main` method and event subscriptions.
- `DAO/UDTTransfer.cs`: Addition of asynchronous data fetching methods.
- `Utility.cs`: Refactoring of `SendData`, `SendDataList`, and `UploadFile` to support asynchronous operations.
- `ImportExport/ImportHighConcern.cs`: Modification of the `Prepare` method and internal data structures.
- `FISCA` and `K12` library dependencies: Continued usage of these frameworks while utilizing their asynchronous support where available.
