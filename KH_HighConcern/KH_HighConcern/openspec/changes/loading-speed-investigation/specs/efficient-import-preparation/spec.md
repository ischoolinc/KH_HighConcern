## ADDED Requirements

### Requirement: Partial Student Data Loading for Import
The system SHALL avoid loading all student records when preparing for an import, instead loading only necessary identification data or utilizing efficient query strategies.

#### Scenario: Opening Import Wizard
- **WHEN** the user opens the "Import High Concern Student" wizard
- **THEN** the wizard prepares its data structures without loading full student records for the entire school, significantly reducing the wait time

### Requirement: Background Validation Data Preparation
The system SHALL prepare validation data in the background during the import wizard's preparation phase.

#### Scenario: Preparing Import
- **WHEN** the import wizard is initializing
- **THEN** it fetches necessary student IDs and high-concern records in a background thread, showing a loading indicator if necessary
