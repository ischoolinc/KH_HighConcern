## Goal

Update the Kaohsiung student basic data import wizard so that Excel files and import UI options can use:

* `家長1` instead of `父親`
* `家長2` instead of `母親`

The program must continue using the existing internal `父親` and `母親` field names for:

* bulk-column matching
* validation
* shift checking
* data reading
* XML generation
* server requests
* database updates

Do not change any unrelated program behavior or the existing XML structure.

After implementation and testing, create or update:

`匯入學生基本資料調整(高雄).md`

## Primary Target File

```text
C:\ischoolProject\KH_HighConcern\StudentImportWizard_KH\StudentImportWizard_KH\StudentImportWizard.cs
```

## Related Classes to Inspect

Inspect the following implementations before deciding where the alias conversion should occur:

* `WizardContext`
* `WizardContext.RefreshImportSource()`
* `SheetColumn`
* `SheetColumnCollection`
* `SheetReader`
* `BulkColumn`
* `BulkColumnCollection`
* `BulkDescription`
* `ValidateColumn`
* `SheetRowSource`

Modify related files only when required to implement a safe source-name-to-internal-name mapping.

Do not modify unrelated classes.

## Required Field Mapping

Support the following mappings:

```text
Excel/UI field             Internal field
家長1姓名             →    父親姓名
家長1身分證號         →    父親身分證號
家長1國籍             →    父親國籍
家長1存歿             →    父親存歿

家長2姓名             →    母親姓名
家長2身分證號         →    母親身分證號
家長2國籍             →    母親國籍
家長2存歿             →    母親存歿
```

If grouped parent-information fields are supported, also support:

```text
家長1其它資訊:*       →    父親其它資訊:*
家長2其它資訊:*       →    母親其它資訊:*
```

Examples:

```text
家長1其它資訊:學歷    →    父親其它資訊:學歷
家長1其它資訊:職業    →    父親其它資訊:職業
家長1其它資訊:電話    →    父親其它資訊:電話

家長2其它資訊:學歷    →    母親其它資訊:學歷
家長2其它資訊:職業    →    母親其它資訊:職業
家長2其它資訊:電話    →    母親其它資訊:電話
```

## Legacy Compatibility

Continue accepting Excel files that use the original headers:

```text
父親姓名
父親身分證號
父親國籍
父親存歿
母親姓名
母親身分證號
母親國籍
母親存歿
```

Both formats must map to the same internal fields:

```text
家長1姓名 or 父親姓名 → 父親姓名
家長2姓名 or 母親姓名 → 母親姓名
```

Do not force users to update existing Excel files.

## Required Design

The program must distinguish between:

```text
SourceName
    The physical header in the Excel file.

DisplayName
    The name displayed in the import UI.

InternalName
    The existing field name used by validation, BulkDescription,
    XML generation, and server requests.
```

Example:

```text
SourceName   = 家長1姓名
DisplayName  = 家長1姓名
InternalName = 父親姓名
```

The physical Excel column must still be read correctly by its original position or source name.

The internal import process must use `父親姓名`.

## Part 1: Normalize Excel Parent Headers

### Current Flow

The source file is loaded through:

```csharp
Context.RefreshImportSource();
```

This method is called:

1. after selecting the source file
2. before validation
3. before final import and XML generation

The alias mapping must remain effective after every source refresh.

### Required Change

Implement a centralized normalization method.

Example:

```csharp
private static string NormalizeParentImportFieldName(
    string fieldName)
{
    if (string.IsNullOrEmpty(fieldName))
        return fieldName;

    if (fieldName.StartsWith("家長1"))
    {
        return "父親" +
            fieldName.Substring("家長1".Length);
    }

    if (fieldName.StartsWith("家長2"))
    {
        return "母親" +
            fieldName.Substring("家長2".Length);
    }

    return fieldName;
}
```

Apply this mapping when Excel columns are converted into `SheetColumn` objects or when the source columns are bound to the existing `BulkColumn` definitions.

Do not apply the mapping only to the `ListView` text.

The normalized internal name must be available in:

```text
Context.SourceColumns
Context.AcceptColumns matching
Context.SelectedFields
Context.ValidateColumns
BulkDescription.Columns.GetColumnList()
```

### Important Source-Reading Constraint

Do not lose the original Excel column reference.

If `SheetReader.GetValue()` depends on the physical header text, preserve the original source name or column index.

For example:

```text
Excel source column: 家長1國籍
Internal lookup name: 父親國籍
```

Calling:

```csharp
reader.GetValue("父親國籍")
```

must still retrieve the data from the Excel column named `家長1國籍`.

Implement an alias lookup or preserve a source-column mapping as needed.

## Part 2: Update the Import Field Selection List

### Current Issue

The current `GroupSheetColumn()` creates the UI item using:

```csharp
item = new ImportItem(each.GroupName);
```

The existing `DisplayColumns()` method also uses:

```csharp
each.Text
```

for both:

* UI display
* internal `AcceptColumns` comparison

This does not safely separate `家長1／家長2` display names from the internal `父親／母親` names.

### Required Change

Update `ImportItem` so it preserves both:

```text
UI display name
Internal group or field name
```

Possible design:

```csharp
private class ImportItem : ListViewItem
{
    private readonly string _internalGroupName;

    public ImportItem(
        string displayName,
        string internalGroupName)
    {
        Text = displayName;
        _internalGroupName = internalGroupName;
    }

    public string InternalGroupName
    {
        get { return _internalGroupName; }
    }
}
```

For non-group fields, use the normalized internal `SheetColumn.Name` for matching.

For grouped fields, use the original internal group name when matching:

```text
父親其它資訊
母親其它資訊
```

The ListView may display:

```text
家長1其它資訊
家長2其它資訊
```

### Required Result

The import field list must display:

```text
家長1姓名
家長1國籍
家長2姓名
家長2國籍
```

But internal comparisons must continue using:

```text
父親姓名
父親國籍
母親姓名
母親國籍
```

Do not mark valid `家長1` or `家長2` Excel fields as unsupported.

## Part 3: Update Identification and Validation Dropdown Options

### Current Issue

The current code directly adds `BulkColumn` objects:

```csharp
foreach (BulkColumn each in Context.AcceptColumns.Values)
{
    if (each.Identifiable)
        cboIdField.Items.Add(each);

    if (each.ShiftCheckable)
        cboValidateField.Items.Add(each);
}
```

The ComboBoxes use:

```csharp
cboIdField.DisplayMember = "DisplayText";
cboValidateField.DisplayMember = "DisplayText";
```

Therefore, the dropdowns display the original internal `BulkColumn.DisplayText` values:

```text
父親姓名
母親姓名
父親國籍
母親國籍
```

### Required Change

Create a display wrapper that contains:

* the original `BulkColumn`
* the UI alias text

Example:

```csharp
private class BulkColumnOption
{
    private readonly BulkColumn _column;
    private readonly string _displayText;

    public BulkColumnOption(
        BulkColumn column,
        string displayText)
    {
        _column = column;
        _displayText = displayText;
    }

    public string DisplayText
    {
        get { return _displayText; }
    }

    public BulkColumn Column
    {
        get { return _column; }
    }
}
```

Add a shared internal-to-display conversion method:

```csharp
private static string GetParentAliasDisplayText(
    string displayText)
{
    if (string.IsNullOrEmpty(displayText))
        return displayText;

    if (displayText.StartsWith("父親"))
    {
        return "家長1" +
            displayText.Substring("父親".Length);
    }

    if (displayText.StartsWith("母親"))
    {
        return "家長2" +
            displayText.Substring("母親".Length);
    }

    return displayText;
}
```

Do not modify the original:

```csharp
BulkColumn.DisplayText
```

### Dropdown Population

Update `wpCollectKeyInfo_AfterPageDisplayed()` so that the ComboBoxes receive wrapper objects.

Example:

```csharp
foreach (BulkColumn each in Context.AcceptColumns.Values)
{
    string displayText =
        GetParentAliasDisplayText(
            each.DisplayText);

    if (each.Identifiable)
    {
        cboIdField.Items.Add(
            new BulkColumnOption(
                each,
                displayText));
    }

    if (each.ShiftCheckable)
    {
        cboValidateField.Items.Add(
            new BulkColumnOption(
                each,
                displayText));
    }
}
```

### Empty Validation Option

Preserve the existing empty validation option.

Wrap it without changing its display or behavior:

```csharp
BulkColumnOption emptyOption =
    new BulkColumnOption(
        Context.EmptyShiftCheckField,
        Context.EmptyShiftCheckField.DisplayText);

cboValidateField.Items.Add(emptyOption);
cboValidateField.SelectedIndex = 0;
```

### Selected Value Handling

Update `wpCollectKeyInfo_NextButtonClick()` so that it retrieves the original `BulkColumn` from the wrapper.

The values saved into the context must remain internal names:

```csharp
Context.IdentifyField =
    idOption.Column.DisplayText;

Context.ShiftCheckField =
    validateOption.Column.DisplayText;
```

Expected behavior:

```text
Dropdown display: 家長1姓名
Context.ShiftCheckField: 父親姓名
```

```text
Dropdown display: 家長2姓名
Context.ShiftCheckField: 母親姓名
```

Do not store the UI aliases in:

```text
Context.IdentifyField
Context.ShiftCheckField
```

## Part 4: Preserve Validation Behavior

Do not modify the existing shift-check lookup:

```csharp
BulkColumn key =
    Context.BulkDescription.Columns[
        Context.IdentifyField];

BulkColumn shift =
    Context.BulkDescription.Columns[
        Context.ShiftCheckField];

checkList = new ShiftCheckList(key, shift);
```

The keys must remain the original internal names.

Do not change:

```csharp
CheckShiftColumn()
CreateValidateColumns()
Validator_ErrorCaptured()
Validator_AutoCorrect()
ValidateKey()
```

except where an alias-aware source lookup is necessary.

## Part 5: Preserve Kaohsiung Nationality Validation

This Kaohsiung version contains:

```csharp
private void ValidateNationColumn(
    Dictionary<int, RowMessage> rowMessages)
```

It currently checks:

```csharp
Context.SelectedFields.ContainsKey(
    "父親國籍");

reader.GetValue(
    "父親國籍");

Context.SelectedFields.ContainsKey(
    "母親國籍");

reader.GetValue(
    "母親國籍");
```

Keep these internal names unchanged.

Expected flow:

```text
Excel: 家長1國籍
Internal field: 父親國籍
ValidateNationColumn: 父親國籍
Generated XML: existing father nationality structure
```

And:

```text
Excel: 家長2國籍
Internal field: 母親國籍
ValidateNationColumn: 母親國籍
Generated XML: existing mother nationality structure
```

Do not change the nationality validation keys to `家長1國籍` or `家長2國籍`.

Verify that `reader.GetValue("父親國籍")` correctly reads the Excel column `家長1國籍` through the alias mapping.

## Part 6: Preserve XML Generation

Do not modify the existing bulk description or XML-generation methods:

```csharp
Context.BulkDescription.Columns.GetColumnList(
    Context.ValidateColumns.GetNames());

bulkdesc.GenerateInsertRequest(
    Context.SourceReader,
    columns,
    record);

bulkdesc.GenerateUpdateRequest(
    Context.SourceReader,
    columns,
    record,
    Context.IdentifyField,
    Context.ShiftCheckField,
    ref_student_id);
```

The generated XML for alias headers must be identical to the XML generated by the original headers.

Example:

```text
Excel A:
父親姓名 = 王大明
母親姓名 = 李小華
```

```text
Excel B:
家長1姓名 = 王大明
家長2姓名 = 李小華
```

Both files must produce the same XML structure and values.

## Kaohsiung-Specific Functions That Must Remain Unchanged

Do not change or break:

* `Gobal._SelectClassName`
* `Gobal._SelectStatus`
* `Gobal._SendData`
* `MsgForm`
* `Utility.SendDataList`
* `Utility.ConveroClassName`
* `ValidateNationColumn`
* class and student-status confirmation dialogs
* Kaohsiung bureau data transmission
* class-lock UDT processing
* student class-change UDT processing
* `UDT_ClassLock`
* `UDT_ClassSpecial`
* `EventHandler` publication for `KH_StudentImportWizard`
* existing insert-mode unchecked behavior
* the required-name field behavior
* student-status validation
* import logging
* password hashing
* class lookup
* diploma-number processing

Do not refactor unrelated high-concern or bureau-reporting logic.

## Important Constraints

Do not modify:

* `JH_S_BulkDescription`
* validation-rule XML
* XML node names
* XML hierarchy
* XML storage structure
* database field names
* service request field names
* existing `BulkColumn.DisplayText`
* existing `BulkDescription.Columns` keys
* internal father field names
* internal mother field names
* server APIs
* unrelated import fields
* unrelated UI behavior

Do not globally replace:

```text
父親 → 家長1
母親 → 家長2
```

The mapping must be limited to:

1. Excel input aliases
2. UI display names

Internal processing must continue using `父親` and `母親`.

## Required Debug Verification

During development, verify the source and internal fields.

Example debug output:

```csharp
System.Diagnostics.Debug.WriteLine(
    "SourceName=" + column.SourceName +
    ", InternalName=" + column.Name +
    ", DisplayText=" + column.DisplayText);
```

Expected:

```text
SourceName=家長1姓名
InternalName=父親姓名
DisplayText=家長1姓名
```

Before XML generation, verify:

```csharp
foreach (string fieldName
    in Context.ValidateColumns.GetNames())
{
    System.Diagnostics.Debug.WriteLine(
        "Internal import field: " +
        fieldName);
}
```

Expected:

```text
Internal import field: 父親姓名
Internal import field: 母親姓名
```

After generating each record, temporarily inspect:

```csharp
System.Diagnostics.Debug.WriteLine(
    record.OuterXml);
```

Remove temporary debugging code after testing unless it follows the project's existing diagnostic conventions.

## Required Test Cases

### Test 1: Alias Headers in Insert Mode

Import an Excel file containing:

```text
姓名
家長1姓名
家長2姓名
```

Verify:

* the fields are recognized
* the fields are enabled
* the fields can be selected
* validation succeeds
* insert succeeds
* the generated XML uses the existing father and mother structure

### Test 2: Alias Headers in Update Mode

Import an Excel file containing:

```text
學生系統編號
家長1姓名
家長2姓名
```

Verify update mode and shift checking.

### Test 3: All Parent Alias Fields

Test:

```text
家長1姓名
家長1身分證號
家長1國籍
家長1存歿
家長2姓名
家長2身分證號
家長2國籍
家長2存歿
```

Verify all mappings.

### Test 4: Parent Information Groups

If supported, test:

```text
家長1其它資訊:學歷
家長1其它資訊:職業
家長1其它資訊:電話
家長2其它資訊:學歷
家長2其它資訊:職業
家長2其它資訊:電話
```

Verify grouped-field acceptance and XML generation.

### Test 5: Validation Dropdown

Verify that the validation dropdown displays:

```text
家長1姓名
家長1身分證號
家長1國籍
家長2姓名
家長2身分證號
家長2國籍
```

Verify the selected internal values remain:

```text
父親姓名
父親身分證號
父親國籍
母親姓名
母親身分證號
母親國籍
```

### Test 6: Empty Validation Option

Verify that selecting the empty validation option produces:

```text
Context.ShiftCheckField = ""
```

### Test 7: Legacy Headers

Verify that Excel files using:

```text
父親姓名
母親姓名
父親國籍
母親國籍
```

continue to work without changes.

### Test 8: Nationality Validation

Test:

```text
家長1國籍
家長2國籍
```

Verify that `ValidateNationColumn()`:

* reads the correct values
* produces warnings correctly
* reports messages against the correct source columns
* does not throw a missing-column exception

### Test 9: XML Comparison

Import equivalent data using:

```text
File A: 父親 / 母親 headers
File B: 家長1 / 家長2 headers
```

Compare the generated XML.

The XML element names, hierarchy, and values must be identical.

### Test 10: Kaohsiung-Specific Regression

Verify that the following still work:

* class import
* status import
* bureau confirmation dialogs
* bureau data transmission
* class-lock processing
* student class-change UDT updates
* nationality validation
* insert mode
* update mode
* import logs
* the `KH_StudentImportWizard` event

### Test 11: Unrelated Fields

Verify no changes to:

```text
姓名
學號
身分證號
學生系統編號
班級
座號
狀態
監護人姓名
監護人國籍
登入帳號
```

## Validation Checklist

* Excel `家長1*` headers are recognized.
* Excel `家長2*` headers are recognized.
* Original `父親*` headers still work.
* Original `母親*` headers still work.
* The import field list displays `家長1／家長2`.
* The validation dropdown displays `家長1／家長2`.
* The identification dropdown displays aliases where applicable.
* `Context.SelectedFields` uses internal names.
* `Context.ValidateColumns` uses internal names.
* `Context.IdentifyField` uses internal names.
* `Context.ShiftCheckField` uses internal names.
* `ValidateNationColumn()` remains functional.
* XML generation remains unchanged.
* XML resources remain unchanged.
* Kaohsiung bureau functions remain unchanged.
* No unrelated logic is modified.
* The solution builds successfully.

## Completion Record

After implementation and testing, create or update:

`匯入學生基本資料調整(高雄).md`

Record:

* modified files
* modified classes and methods
* source-header alias implementation
* UI display alias implementation
* validation-dropdown wrapper implementation
* final parent mapping table
* how the original Excel header is preserved
* how the internal field name is preserved
* how `SheetReader` resolves aliases
* confirmation that legacy headers remain supported
* confirmation that `ValidateNationColumn()` remains unchanged
* confirmation that Kaohsiung-specific logic remains unchanged
* confirmation that XML resources were not modified
* confirmation that generated XML remains unchanged
* insert-mode test results
* update-mode test results
* validation-dropdown test results
* nationality-validation test results
* legacy-header test results
* XML comparison results
* Kaohsiung-specific regression test results
* any issues or limitations found
