## Why

The application currently experiences performance lag due to frequent and redundant data reloads triggered by multiple system events. Every time a student's status or class information changes, the entire class lock and student count dataset is re-fetched and recalculated, leading to a suboptimal user experience, especially in environments with large amounts of data.

## What Changes

- **Event Debouncing**: Implement a debouncing mechanism for the `FISCA.InteractionService` event subscriptions to prevent rapid-fire reloads.
- **Background Loading Optimization**: Move one-time initialization tasks (like UDT schema synchronization) out of the frequent background worker loop.
- **Efficient Data Refresh**: Refactor the reload logic to minimize database hits and unnecessary re-calculations.
- **Conditional Initialization**: Ensure that certain checks (like district unlock notifications) only run when necessary rather than on every minor data update.

## Capabilities

### New Capabilities
- `loading-optimization`: Implements a throttled/debounced reloading strategy and optimizes startup data fetching to ensure the UI remains responsive during data updates.

### Modified Capabilities
- None.

## Impact

- `Program.cs`: Modification of event handlers and background worker logic.
- `DAO/UDTTransfer.cs`: Optimization of schema synchronization and data retrieval methods.
- `Utility.cs`: Refinement of utility checks to reduce overhead.
