function setEnabled(enabled) {
    if (enabled) {
        $(".k-add-button").removeClass("k-state-disabled").addClass("k-grid-add");
    }
    else {
        $(".k-add-button").addClass("k-state-disabled").removeClass("k-grid-add");
    }
}

function OpenDrowdownEditorWindow() {
    var myWindow = $("#window");

    myWindow.kendoWindow({
        width: "600px",
        height: "600px",
        title: "DropDown Editor",
        visible: false,
        open: function () {
            $("#comboDropDownItems").kendoDropDownList({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [{ text: "Gender", value: 1 }, { text: "Incident Type", value: 2 },
                { text: "Incident Location", value: 3 }, { text: "Disposition", value: 4 }, { text: "Report Written By", value: 5 }, {
                    text: "Report Reviewed By", value: 6
                }, { text: "Equipment Type", value: 7 }, { text: "Equipment Status", value: 8 }, { text: "Person Of Interest Type", value: 9 }
                ],
                optionLabel: "Select Dropdown type",
                change: function (e) {
                    $('#gridDropDownItems').data('kendoGrid').dataSource.read();
                    $(".k-grid-toolbar").show();
                }
            });
            var grid = $("#gridDropDownItems").kendoGrid({
                height: 450,
                sortable: true,
                autoBind: false,
                selectable: "single",
                toolbar: ["create", "save", "cancel"],
                navigatable: true,
                editable: true,
                dataBound: function () {

                },
                columns: [
                    { field: "TableId", title: "Id", width: 20 },
                    { field: "Description", title: "Description", width: 200 },
                    {
                        field: "Active", title: "Active", width: 75, template:
                            function (dataItem) {
                                if (dataItem.Active)
                                    return "<input type='checkbox' checked='checked'  class='chkbx'/>";
                                else
                                    return "<input type='checkbox' class='chkbx' />";
                            }
                    }
                ],
                dataSource:
                    new kendo.data.DataSource({
                        batch: true,
                        schema: {
                            model: {
                                id: "TableId",
                                fields: {
                                    TableId: { editable: false, nullable: false },
                                    Description: { validation: { required: true } },
                                    Active: { type: "boolean", defaultValue: true, },
                                }
                            }
                        },
                        transport: {
                            create: function (option) {
                                $('#window').block({ message: 'Loading...</h1>' });
                                $.ajax({
                                    type: 'POST',
                                    url: url_UpdateGridData,
                                    data: { tableId: $("#comboDropDownItems").data("kendoDropDownList").value(), gridData: option.data.models },
                                    dataType: "json",
                                    success: function (result) {
                                        option.success(result);
                                        var grid = $("#gridDropDownItems").data("kendoGrid");
                                        grid.dataSource.read();
                                        $('#window').unblock();
                                    },
                                    error: function (result) {
                                        option.error(result);
                                    }
                                });
                            },
                            update: function (option) {
                                $('#window').block({ message: 'Loading...</h1>' });
                                $.ajax({
                                    type: 'POST',
                                    url: url_UpdateGridData,
                                    data: { tableId: $("#comboDropDownItems").data("kendoDropDownList").value(), gridData: option.data.models },
                                    dataType: "json",
                                    success: function (result) {
                                        option.success(result);
                                        var grid = $("#gridDropDownItems").data("kendoGrid");
                                        grid.dataSource.read();
                                        $('#window').unblock();
                                    },
                                    error: function (result) {
                                        option.error(result);
                                    }
                                });
                            },

                            read: function (options) {
                                $('#window').block({ message: 'Loading...</h1>' });
                                $.ajax({
                                    type: 'POST',
                                    url: url_GetGridData,
                                    data: { tableId: $("#comboDropDownItems").data("kendoDropDownList").value() },
                                    dataType: "json",
                                    success: function (result) {
                                        options.success(result);
                                        $('#window').unblock();
                                    },
                                    error: function (result) {
                                        options.error(result);
                                    }
                                });
                            }
                        }
                    })
            });

            $("#btn_Cancel").kendoButton({
                click: function () {
                    $("#window").data("kendoWindow").close();
                }
            });

            $("#gridDropDownItems .k-grid-content").on("change", "input.chkbx", function (e) {
                var grid = $("#gridDropDownItems").data("kendoGrid"),
                    dataItem = grid.dataItem($(e.target).closest("tr"));

                dataItem.set("Active", this.checked);
            });

            $(".k-grid-toolbar").hide();

        },
        actions: [
            //"Pin",
            //"Minimize",
            //"Maximize",
            "Close"
        ]
    }).data("kendoWindow");

    var template = kendo.template($("#templateDropDownEditor").html());
    myWindow.data("kendoWindow")
        .content(template({}))
        .center().open();
}
$(document).ready(function () {
    $("#btn_Dropdown").kendoButton({
        click: function () {
            OpenDrowdownEditorWindow();
        }
    });
});