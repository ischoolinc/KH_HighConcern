## 1. DAO Optimization

- [x] 1.1 Add a static `_isSchemaSynced` boolean flag to `UDTTransfer` class in `DAO/UDTTransfer.cs`.
- [x] 1.2 Modify `UDTTransfer.CreateUDTTable()` to check and set the `_isSchemaSynced` flag, ensuring it only runs once.

## 2. Event Handling Optimization

- [x] 2.1 Declare a static `System.Windows.Forms.Timer` named `_debounceTimer` and a boolean `_reloadPending` in `Program.cs`.
- [x] 2.2 Initialize `_debounceTimer` in `Main` with a 500ms interval and a `Tick` handler that triggers `_bgLLoadUDT.RunWorkerAsync()`.
- [x] 2.3 Implement a `TriggerReload()` helper method in `Program.cs` that resets the timer.
- [x] 2.4 Update all `FISCA.InteractionService.SubscribeEvent` handlers in `Program.cs` to call `TriggerReload()` instead of immediate reloads.
- [x] 2.5 In `_bgLLoadUDT_RunWorkerCompleted`, check if `_reloadPending` is true and trigger another reload if necessary.

## 3. Testing and Validation

- [x] 3.1 Verify that rapid student updates no longer cause immediate, multiple reloads.
- [x] 3.2 Ensure UI columns still refresh correctly after the debounced delay.
- [x] 3.3 Confirm that UDT tables are still correctly initialized on the first run.
