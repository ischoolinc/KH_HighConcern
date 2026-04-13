## 1. Asynchronous Initialization in Program.cs

- [x] 1.1 Add a new `BackgroundWorker` (e.g., `_bgLoadData`) specifically for fetching `_HighConcernDict` in `Program.Main`.
- [x] 1.2 Refactor `Program.Main` to move the synchronous `UDTTransfer.GetHighConcernDictAll()` call into the `DoWork` event of `_bgLoadData`.
- [x] 1.3 Update `RunWorkerCompleted` for `_bgLoadData` to refresh the `ListPaneField`s once the data is loaded.
- [x] 1.4 Ensure `FISCA.InteractionService.SubscribeEvent` handler uses the background worker or an asynchronous pattern to refresh data.

## 2. Utility Class Enhancements

- [x] 2.1 Implement `SendDataAsync` and `SendDataListAsync` in `Utility.cs` using asynchronous `HttpWebRequest` patterns (e.g., `Task.Run` or `BeginGetResponse`).
- [x] 2.2 Implement `UploadFileAsync` in `Utility.cs` to handle file uploads in the background.
- [x] 2.3 Update `DetailContent/HighConcernContent.cs` and `ImportExport/ImportHighConcern.cs` to use the new asynchronous utility methods.

## 3. Import Wizard Optimization

- [x] 3.1 Optimize `UDTTransfer.GetStudentNumIDDictAll()` to be more efficient if possible, or ensure it's called in a way that doesn't block the UI unnecessarily.
- [x] 3.2 Refactor `ImportHighConcern.Prepare` to load student data lazily or in a background thread if the framework supports it.
- [x] 3.3 Optimize the query in `ImportHighConcern.Prepare` to avoid loading full `StudentRecord` objects for all students when only basic info is needed.

## 4. Verification and Testing

- [x] 4.1 Verify that the application starts up without blocking the UI thread.
- [x] 4.2 Verify that high-concern columns in the student list are eventually populated after startup.
- [x] 4.3 Test the import wizard to ensure it loads faster and remains responsive.
- [x] 4.4 Test saving and uploading documents to ensure no UI hangs occur.
