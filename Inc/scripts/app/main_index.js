
function DeleteRow(e) {
    var gviewpoi = $("#gridPicturesPOI").data("kendoGrid");
    var row = e.select().closest("tr");
    gviewpoi.removeRow(row);

}
function DeleteRowEquipment(e) {
    var gviewEquip = $("#gridFileUpload").data("kendoGrid");
    var row = e.select().closest("tr");
    gviewEquip.removeRow(row);

}
$(document).ready(function () {
    var grid = $("#grid").kendoGrid({
        toolbar: kendo.template($("#template").html()),
        sortable: true,
        selectable: "single",
        columns: [
            { field: "Id", title: "FileId", hidden: true },
            { field: "Incident_Type.Description", title: "Incident" },
            { field: "Incident_Location.Description", title: "Location" },
            { field: "Complainant", title: "Complainant/Reported By", template: function (a) { return a.Complainant.Last_Name + "," + a.Complainant.First_Name; } },
            { field: "Report_Date", title: "Date/Time Reported or Entered" },
            { field: "Incident_Occurance_Date", title: "Date/Time Occurred or Discovered" }
        ],
        dataSource:
        new kendo.data.DataSource({
            transport: {
                read: function (options) {
                    $("#grid").block({ message: 'Loading...</h1>' });
                    $.ajax({
                        type: "POST",
                        url: url_GetMaingridComplaint,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        //data: JSON.stringify({ Complaint_Id: 1}),
                        success: function (data) {
                            options.success(data);
                            $("#grid").unblock();
                        }
                    });
                }
            }
        })
    });

    $("#btn_New_Complaint").kendoButton({
        click: function () {
            OpenIncidentWindow(null);
        }
    });

    $("#grid").delegate("tbody>tr", "dblclick", function () {
        $("#grid").block({ message: 'Loading...</h1>' });
        var gview = $("#grid").data("kendoGrid");
        var selectedItem = gview.dataItem(gview.select());
       
        $.ajax({
            type: "POST",
            url: url_GetComplaint,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ComplaintId: selectedItem.Id }),
            success: function (result) {
                OpenIncidentWindow(result);
                $("#grid").unblock();
            }
        });
    });

    function OpenIncidentWindow(data) {
        var myWindow = $("#window");
        myWindow.kendoWindow({
            width: "800px",
            height: "685px",
            modal: true,
            title: data == null ? "New Window" : "Incident File Id " + data.Id,
            visible: false,
            open: function () {
                $("#comboIncidents").kendoDropDownList({
                    optionLabel: "Select Any Value",
                    dataTextField: "Description",
                    dataValueField: "TableId",
                    dataSource:comboDatastore.dataIncidentType,
                    value: data == null ? 0 : data.Incident_Type.Id

                });
                $("#comboLocation").kendoDropDownList({
                    optionLabel: "Select Any Value",
                    dataTextField: "Description",
                    dataValueField: "TableId",
                    dataSource: comboDatastore.dataLocation,
                    value: data == null ? 0 : data.Incident_Location.Id
                });
                $("#comboGender").kendoDropDownList({
                    optionLabel: "Select Any Value",
                    dataTextField: "Description",
                    dataValueField: "TableId",
                    dataSource: comboDatastore.dataGender,
                    value: data == null ? 0 : data.Complainant.Gender.Id
                });

                $("#comboDisposition").kendoDropDownList({
                    optionLabel: "Select Any Value",
                    dataTextField: "Description",
                    dataValueField: "TableId",
                    dataSource: comboDatastore.dataDisposition,
                    value: data == null ? 0 : data.Disposition.Id

                });
                $("#comboReportWrittenBy").kendoDropDownList({
                    optionLabel: "Select Any Value",
                    dataTextField: "Description",
                    dataValueField: "TableId",
                    dataSource: comboDatastore.dataWritten_By,
                    value: data == null ? 0 : data.Report_Written_By.Id
                });
                $("#comboReportReviewedBy").kendoDropDownList({
                    optionLabel: "Select Any Value",
                    dataTextField: "Description",
                    dataValueField: "TableId",
                    dataSource: comboDatastore.dataReport_Reviewed_By,
                    value: data == null ? 0 : data.Report_Reviewed_By.Id
                });

                $("#dtDOB").kendoDatePicker({
                    value: data == null ? new Date() : data.Complainant.DOB,
                    dateInput: true
                });
                $("#dtCreate").kendoDateTimePicker({
                    value: data == null ? "" : data.Report_Date,
                    dateInput: true
                });
                $("#dtOccurance").kendoDateTimePicker({
                    value: data == null ? "" : data.Incident_Occurance_Date,
                    dateInput: true
                });
                $("#btn_Print").kendoButton({
                    click: function () {
                        var RptId = data.Id;
                        $.ajax({
                            type: "GET",
                            url: 'Reports/PrintReport?RptId='+data.Id,                         
                            success:function()
                            {
                                window.location = 'Reports/PrintReport?RptId='+data.Id   
                            }
                        });
                    }
                });
                $("#btnCancel").kendoButton({
                    click: function () {
                        $("#window").data("kendoWindow").close();
                    }
                });
                $("#btn_Add_Equipment").kendoButton({
                    click: function () {
                        OpenEquipmentWindow(null);
                    }
                });
                $("#btn_Add_POI").kendoButton({
                    click: function () {
                        OpenPOIWindow(null);
                    }
                });

                var tabstrip = $("#tabstrip").kendoTabStrip().data("kendoTabStrip");

                var grid = $("#gridSuspects").kendoGrid({
                    height: 100,
                    sortable: true,
                    selectable: "single",
                    columns: [
                        { field: "Id", title: "Id", hidden: true },
                        { field: "Type.Id", title: "Type Id", hidden: true },
                        { field: "Type.Description", title: "POI Type" },
                        { field: "Person.Id", title: "Person Id", hidden: true },
                        { field: "Person.First_Name" + "," + "Person.Last_Name", title: "Person Name" },
                        { field: "Comments", title: "Comments" }
                    ],
                    dataSource: data == null ? [] : data.Person_Of_Interest
                });
                var grid1 = $("#gridItems").kendoGrid({
                    height: 100,
                    sortable: true,
                    selectable: "single",
                    columns: [
                        { field: "Id", title: "Id", hidden: true },
                        { field: "Type.Id", title: "Type Id", hidden: true },
                        { field: "Status.Id", title: "Status Id", hidden: true },
                        { field: "Type.Description", title: "Equipment Type" },
                        { field: "Status.Description", title: "Equipment Status" },
                        { field: "Value", title: "Value" },
                        { field: "Occurance_Date", title: "Occurance_Date" },
                        { field: "Description", title: "Description" }
                    ],
                    dataSource: data == null ? [] : data.Equipment
                });
                tabstrip.one("activate", function () {
                    grid.resize();
                    grid1.resize();
                });

                $("#context-menu").kendoContextMenu({
                    target: "#gridItems",
                    filter: "tbody tr td",
                    open: function (e) {
                        var gviewItems = $("#gridItems").data("kendoGrid");
                        var selectedItem = gviewItems.dataItem(gviewItems.select());
                        var dataSourceItems = [{
                            text: "Delete Equipment",
                            key: "d"
                        }];
                        e.sender.setOptions({
                            dataSource: dataSourceItems
                        });
                    },
                    select: function (e) {
                        var gviewItems = $("#gridItems").data("kendoGrid");
                        var selectedItem = gviewItems.dataItem(gviewItems.select());
                        gviewItems.removeRow($(this));
                        gviewItems.dataSource.remove(selectedItem);
                        $("context-menu").hide();
                    }
                });
                $(".k-grid").on("mousedown", "tr[role='row']", function (e) {
                    $(this).siblings().removeClass("k-state-selected");
                    $(this).addClass("k-state-selected");
                });
                $("#context-menu").kendoContextMenu({
                    target: "#gridSuspects",
                    filter: "tbody tr td",
                    open: function (e) {
                        var gviewSuspect = $("#gridSuspects").data("kendoGrid");
                        var selectedItem = gviewSuspect.dataItem(gviewSuspect.select());
                        var dataSourceItems = [{
                            text: "Delete Suspect",
                            key: "d"
                        }];
                        e.sender.setOptions({
                            dataSource: dataSourceItems
                        });
                    },
                    select: function (e) {
                        var gviewSuspect = $("#gridSuspects").data("kendoGrid");
                        var selectedItem = gviewSuspect.dataItem(gviewSuspect.select());
                        gviewSuspect.removeRow($(this));
                        gviewSuspect.dataSource.remove(selectedItem);
                        $("context-menu").hide();
                    }
                });
                $(".k-grid").on("mousedown", "tr[role='row']", function (e) {
                    $(this).siblings().removeClass("k-state-selected");
                    $(this).addClass("k-state-selected");
                });

                $("#gridItems").delegate("tbody>tr", "dblclick", function () {
                    var gviewItems = $("#gridItems").data("kendoGrid");
                    var selectedItem = gviewItems.dataItem(gviewItems.select());

                    OpenEquipmentWindow(selectedItem);
                });
                $("#gridSuspects").delegate("tbody>tr", "dblclick", function () {
                    var gviewSuspects = $("#gridSuspects").data("kendoGrid");
                    var selectedItem = gviewSuspects.dataItem(gviewSuspects.select());
                    OpenPOIWindow(selectedItem);
                });

                $("#btn_Save_Changes").kendoButton({
                    click: function () {
                        var SuspectData = $("#gridSuspects").data().kendoGrid.dataSource.view();
                        var SuspectsArray = [];
                        for (var i = 0; i < SuspectData.length; i++) {
                            var poi_photo = [];
                            for (var k = 0; k < SuspectData[i].Pictures.length; k++) {
                                poi_photo.push({ Id: 0, Photo: SuspectData[i].Pictures[k].Photo, Description: SuspectData[i].Pictures[k].Description })
                            }
                            Person_of_Interest = {

                                Id: SuspectData[i].Id,
                                Comments: SuspectData[i].Comments,
                                Person: {
                                    Id: SuspectData[i].Person.Id,
                                    Last_Name: SuspectData[i].Person.Last_Name,
                                    First_Name: SuspectData[i].Person.First_Name,
                                    Middle_Name: SuspectData[i].Person.Middle_Name,
                                    DOB: SuspectData[i].Person.DOB,
                                    Street: SuspectData[i].Person.Street,
                                    City: SuspectData[i].Person.City,
                                    State: SuspectData[i].Person.State,
                                    Zip: SuspectData[i].Person.Zip,
                                    Home_Number: SuspectData[i].Person.Home_Number,
                                    Mobile_Number: SuspectData[i].Person.Mobile_Number,
                                    Other_Number: SuspectData[i].Person.Other_Number,
                                    AKA: SuspectData[i].Person.AKA,
                                    Gender: {
                                        Id: SuspectData[i].Person.Gender.Id
                                    }
                                },
                                Type: {
                                    Id: SuspectData[i].Type.Id,
                                    Description: SuspectData[i].Type.Description
                                },
                                Pictures: poi_photo
                            }
                            SuspectsArray.push(Person_of_Interest);
                        }
                        var EquipmentData = $("#gridItems").data().kendoGrid.dataSource.view();
                        var EquipmentArray = [];
                        for (var i = 0; i < EquipmentData.length; i++) {

                            var eq_photo = [];
                            for (var j = 0; j < EquipmentData[i].Pictures.length; j++) {
                                eq_photo.push({ Id: 0, Photo: EquipmentData[i].Pictures[j].Photo, Description: EquipmentData[i].Pictures[j].Description })
                            }
                            Equipment = {
                                Id: EquipmentData[i].Id,
                                Description: EquipmentData[i].Description,
                                Value: EquipmentData[i].Value,
                                Occurance_Date: EquipmentData[i].Occurance_Date,
                                Status:
                                {
                                    Id: EquipmentData[i].Status.Id,
                                    Description: EquipmentData[i].Status.Description
                                },
                                Type: {
                                    Id: EquipmentData[i].Type.Id,
                                    Description: EquipmentData[i].Type.Description
                                },
                                Pictures: eq_photo
                            }
                            EquipmentArray.push(Equipment);
                        }
                        var L_Name = $("#txtComplainantLastName").val(),
                            F_Name = $("#txtComplainantFirstName").val(),
                            M_Name = $("#txtComplainantMiddleName").val(),
                            //aka = $("#txtAKA").val(),
                            dob = $("#dtDOB").val(),
                            Street = $("#txtStreet").val(),
                            State = $("#txtState").val(),
                            City = $("#txtCity").val(),
                            Home = $("#txtHome").val(),
                            Mobile = $("#txtMobile").val(),
                            Other = $("#txtOther").val(),
                            Zip = $("#txtZip").val(),
                            Gender = $("#comboGender").data("kendoDropDownList").value();
                            dt_occurance = $("#dtOccurance").val(),
                            dt_create = $("#dtCreate").val(),
                            narrative = $("#editor").val(),
                            DispositionId = $("#comboDisposition").data("kendoDropDownList").value(),
                            IncidentTypeId = $("#comboIncidents").data("kendoDropDownList").value(),
                            Inc_Location_Id = $("#comboLocation").data("kendoDropDownList").value(),
                            rpt_rev_by = $("#comboReportReviewedBy").data("kendoDropDownList").value(),
                            rpt_written_by = $("#comboReportWrittenBy").data("kendoDropDownList").value(),

                            test = {
                                Id: data == null ? 0 : data.Id,
                                Report_Date: dt_create,
                                Incident_Occurance_Date: dt_occurance,
                                Be_Id:$("#txtBENumber").val(),
                                Narrative: narrative,
                                Equipment: EquipmentArray,
                                Person_Of_Interest: SuspectsArray,
                                Disposition:
                                {
                                    Id: DispositionId
                                },
                                Incident_Location: {
                                    Id: Inc_Location_Id
                                },
                                Incident_Type: {
                                    Id: IncidentTypeId
                                },

                                Report_Reviewed_By:
                                {
                                    Id: rpt_rev_by
                                },
                                Report_Written_By:
                                {
                                    Id: rpt_written_by
                                },
                                Complainant: {
                                    //AKA:aka,
                                    Last_Name: L_Name,
                                    First_Name: F_Name,
                                    Middle_Name: M_Name,
                                    DOB: dob,
                                    Id: data == null ? 0 : data.Complainant.Id,
                                    Gender:
                                    {
                                        Id: Gender
                                    },
                                    Street: Street,
                                    City: City,
                                    State: State,
                                    Zip: Zip,
                                    Home_Number: Home,
                                    Other_Number: Other,
                                    Mobile_Number: Mobile
                                }

                            };
                            if (L_Name == "" || IncidentTypeId == "" || Inc_Location_Id == "" || F_Name == "" || DispositionId == 0 || rpt_written_by == "" || narrative == "" || dt_create == "" || dt_occurance == "" || $("#txtBENumber").val()=="")
                            {
                                if (IncidentTypeId == "")
                                { alert("Incident Type is Required"); return false; }
                                if (dt_create == "")
                                { alert("Report Date is Required"); return false; }
                                if (dt_occurance == "")
                                { alert("Occurance Date is Required"); return false; }
                                if (Inc_Location_Id == "")
                                { alert("Incident Location is Required"); return false; }
                                if (L_Name == "")
                                { alert("Last Name is Required"); return false; }
                               
                                if (F_Name == "")
                                { alert("First Name is Required"); return false; }
                                if ($("#txtBENumber").val() == "")
                                { alert("BE Number is Required"); return false; }
                                if (DispositionId == "")
                                { alert("Disposition is Required"); return false; }
                                if (rpt_written_by == "")
                                { alert("Report Written By is Required"); return false; }
                                if (narrative == "")
                                { alert("Narrative is Required"); return false; }
                            
                        }
                        else {
                          $("#window").block({ message: 'Loading...</h1>' });
                            $.ajax({
                                type: 'POST',
                                url: url_InsertComplaint,
                                data: test,
                                dataType: "json",
                                success: function (result) {
                                    $("#window").data("kendoWindow").close();
                                    $("#grid").data().kendoGrid.dataSource.read();
                                    $("#window").unblock();
                                },
                                error: function (result) {
                                    /// option.error(result);
                                }
                            });
                        }                       
                    }
                });
            },
            actions: [
                //"Pin",
                //"Minimize",
                //"Maximize",
                "Close"
            ]
        }).data("kendoWindow");

        var template = kendo.template($("#password-validation").html());
        myWindow.data("kendoWindow")

            .content(template(data ? data : {}))
            .center().open();

        myWindow
            .find(".password-ok")
            .click(function () {
                kendoWindow.data("kendoWindow").close();
            })
            .end()

        $("#editor").kendoEditor({
            //resizable: {
            //    content: true,
            //    toolbar: true,
                             
            //},
            tools: [
              "bold",
              "italic",
              "underline",
              "foreColor"
            ]
        });
    }

    function OpenEquipmentWindow(data) {
        var myEquipmentWindow = $("#windowequipment");

        myEquipmentWindow.kendoWindow({
            width: "400px",
            height: "600px",
            modal: true,
            title: data == null ? "New Equipment" : "Equpiment Id:" + data.Id,
            visible: false,
            open: function () {
                $("#comboEquipmentType").kendoDropDownList({
                    optionLabel: "Select Any Value",
                    dataTextField: "Description",
                    dataValueField: "TableId",
                    dataSource: comboDatastore.dataEquipmentType,
                    value: data == null ? 0 : data.Type.Id

                });
                $("#comboEquipmentStatus").kendoDropDownList({
                    optionLabel: "Select Any Value",
                    dataTextField: "Description",
                    dataValueField: "TableId",
                    dataSource: comboDatastore.dataEquipmentStatus,
                    value: data == null ? 0 : data.Status.Id
                });

                $("#dtOcc").kendoDatePicker({
                    value: data == null ? "" : data.Occurance_Date,
                    dateInput: true
                });
                $("#fileEquipment").kendoUpload({
                    multiple: true,
                    showFileList: false,                  
                    async: {
                        saveUrl: url_ConvertB64,
                        removeUrl: url_RemoveImage,
                    },
                    select: onSelect,
                    success: onSuccess,
                    validation: {
                        allowedExtensions: [".gif", ".jpeg", ".jpg", ".png"]
                    }//,
                    //template: kendo.template($('#fileTemplate').html())
                });

                function onSelect(e) {
                    $("#windowequipment").block({ message: 'Loading...</h1>' });
                    var files = e.files
                    var acceptedFiles = [".jpg", ".jpeg", ".png", ".gif"]
                    var isAcceptedImageFormat = ($.inArray(files[0].extension, acceptedFiles)) != -1
                    if (!isAcceptedImageFormat) {
                        e.preventDefault();
                        alert("Image must be jpeg, jpg, png or gif");
                    }                  
                }

                function onSuccess(e) {
                
                    var grid = $("#gridFileUpload").data("kendoGrid");
                    if (grid) {
                        var dataSource = grid.dataSource;
                        var total = dataSource.data().length;
                        dataSource.insert(total, { "Photo": e.response });
                    }
                    $("#windowequipment").unblock();
                }

                var gridFileUpload = $("#gridFileUpload").kendoGrid({
                    sortable: true,
                    height: 250,
                    editable: true,
                    columns: [
                        {
                            field: "Id", title: "Id", hidden: true
                        },
                        {
                            field: "Photo", title: "Photo", width: 60, template: function (a) {
                                return "<div class='hasTooltip' ><img src='" + a.Photo + "' height='40px'/></div>"
                            }
                        },
                        {
                            field: "Description", title: "Description"
                        },
                        {
                            template: "<button type='button' onclick='DeleteRowEquipment($(this))'>Delete</button>"
                        }
                    ],
                    dataSource: new kendo.data.DataSource({
                        data: data == null ? [] : data.Pictures,
                        schema: {
                            model: {
                                id: "Id",
                                fields: {
                                    Id: { editable: false },
                                    Photo: { editable: false },
                                    Description: { editable: true, validation: { required: true, min: 1 } }
                                },
                            }
                        }
                    })
                });

                $("#gridFileUpload").kendoTooltip({
                    autoHide: true,
                    showOn: "click",
                    width: 100,
                    height: 100,
                    position: "top",
                    filter: ".k-grid-content div.hasTooltip",
                    content: function (e) {
                        var template = kendo.template($("#storeTerritory").html());
                        return template({ Photo: $(e.target).children('img').attr('src') });
                    }
                });

                $("#btn_Equ_Cancel").kendoButton({
                    click: function () {
                        $("#windowequipment").data("kendoWindow").close();
                    }
                });

                $("#btn_Save_Equipment").kendoButton({
                    click: function () {

                        var gridItems = $("#gridItems").data("kendoGrid");
                        var validatable = $("#gridFileUpload").data().kendoGrid.dataSource.view();
                        for (var i = 0; i < validatable.length; i++) {
                            var desc = validatable[i].Description;
                        }
                        if (validatable.length == 0)
                        {
                            desc = -1;
                        }
                        if ($("#comboEquipmentStatus").data("kendoDropDownList").value() != "" && $("#dtOcc").val() != ""&& (desc != undefined || desc == -1))
                        {
                            if (data == null) {
                                gridItems.dataSource.add({
                                    "Id": 0,
                                    "Type": {
                                        "Id": $("#comboEquipmentType").data("kendoDropDownList").value(),
                                        "Description": $("#comboEquipmentType").data("kendoDropDownList").text()
                                    },
                                    "Status":
                                    {
                                        "Id": $("#comboEquipmentStatus").data("kendoDropDownList").value(),
                                        "Description": $("#comboEquipmentStatus").data("kendoDropDownList").text()
                                    },
                                    "Value": $("#txtValue").val(),
                                    "Occurance_Date": $("#dtOcc").val(),
                                    "Description": $("#txtDesc").val(),
                                    "Pictures": $("#gridFileUpload").data().kendoGrid.dataSource.view()

                                });
                            }
                            else {
                                data.Value = $("#txtValue").val();
                                data.Occurance_Date = $("#dtOcc").val();
                                data.Description = $("#txtDesc").val();
                                data.Type.Id = $("#comboEquipmentType").data("kendoDropDownList").value();
                                data.Type.Description = $("#comboEquipmentType").data("kendoDropDownList").text();
                                data.Status.Id = $("#comboEquipmentStatus").data("kendoDropDownList").value();
                                data.Status.Description = $("#comboEquipmentStatus").data("kendoDropDownList").text();
                                data.Pictures = $("#gridFileUpload").data().kendoGrid.dataSource.view();
                                //dataItem = gridItems.dataItem(gridItems.select());
                                //dataItem.set("Value", $("#txtValue").val());
                                //dataItem.set("Occurance_Date", $("#dtOcc").val());
                                //dataItem.set("Description", $("#txtDesc").val());
                                //dataItem.set("Type.Id", $("#comboEquipmentType").data("kendoDropDownList").value());
                                //dataItem.set("Status.Id", $("#comboEquipmentStatus").data("kendoDropDownList").value());
                                //dataItem.set("Type.Description", $("#comboEquipmentType").data("kendoDropDownList").text());
                                //dataItem.set("Status.Description", $("#comboEquipmentStatus").data("kendoDropDownList").text());
                                //dataItem.set("Pictures", $("#gridFileUpload").data().kendoGrid.dataSource.view());
                            }
                            $("#windowequipment").data("kendoWindow").close();
                            return false;
                        }
                        else {
                            if ($("#comboEquipmentStatus").data("kendoDropDownList").value() == "") {
                                alert("Equipment Status is Required" );
                                return false;
                            }
                            if ($("#dtOcc").val() == "") {
                                alert("Occurance Date is Required");
                                return false;
                            }
                            if(desc == undefined)
                            {
                                alert("Photo Description is Required");
                                return false;
                            }                                                     
                        }
                     
                    }
                });
            },
            actions: [
                //"Pin",
                //"Minimize",
                //"Maximize",
                "Close"
            ]
        }).data("kendoWindow");

        var template = kendo.template($("#equipment").html());
        myEquipmentWindow.data("kendoWindow")

            .content(template(data ? data : {}))
            .center().open();

        myEquipmentWindow
            .find(".password-ok")
            .click(function () {
                kendoWindow.data("kendoWindow").close();
            })
            .end()

    }
    function OpenPOIWindow(data) {
        var myPOIWindow = $("#windowPOI");
        myPOIWindow.kendoWindow({
            width: "700px",
            height: "600px",
            modal: true,
            title: data == null ? "New Person Of Interest" : "Person Of Interest Id :" + data.Id,
            visible: false,
            open: function () {
                $("#comboPOIType").kendoDropDownList({
                    optionLabel: "Select Any Value",
                    dataTextField: "Description",
                    dataValueField: "TableId",
                    dataSource: comboDatastore.dataPOIType,
                    value: data == null ? 0 : data.Type.Id

                });
                $("#comboPOIGender").kendoDropDownList({
                    optionLabel: "Select Any Value",
                    dataTextField: "Description",
                    dataValueField: "TableId",
                    dataSource: comboDatastore.dataGender,
                    value: data == null ? 0 : data.Person.Gender.Id
                });
                $("#dtPOIDOB").kendoDatePicker({
                    value: data == null ? new Date() : data.Person.DOB,
                    dateInput: true
                });

                $("#filePOI").kendoUpload({
                    multiple: true,
                    showFileList: false,
                    async: {
                        saveUrl: url_ConvertB64,
                        removeUrl: url_RemoveImage,
                    },
                    select: onSelect,
                    success: onSuccess,
                    validation: {
                        allowedExtensions: [".gif", ".jpeg", ".jpg", ".png"]
                    }//,
                    //template: kendo.template($('#fileTemplate').html())
                });

                function onSelect(e) {
                    $('#windowPOI').block({ message: 'Loading...</h1>' });
                    var files = e.files
                    var acceptedFiles = [".jpg", ".jpeg", ".png", ".gif"]
                    var isAcceptedImageFormat = ($.inArray(files[0].extension, acceptedFiles)) != -1

                    if (!isAcceptedImageFormat) {
                        e.preventDefault();
                        alert("Image must be jpeg, jpg, png or gif");
                    }
                }

                function onSuccess(e) {
                    var gridPOI = $("#gridPicturesPOI").data("kendoGrid");
                    if (gridPOI) {
                        var dataSource = gridPOI.dataSource;
                        var total = dataSource.data().length;
                        dataSource.insert(total, { "Photo": e.response });
                    }
                    $('#windowPOI').unblock();
                }

                var gridPicturesPOI = $("#gridPicturesPOI").kendoGrid({
                    sortable: true,
                    height: 220,
                    editable: true,
                    columns: [
                        {
                            field: "Id", title: "Id", hidden: true
                        },
                        {
                            field: "Photo", title: "Img", width: 60, template: function (a) {
                                return "<div class='hasTooltip' ><img src='" + a.Photo + "' height='40px'/></div>"
                            }
                        },
                        {
                            field: "Description", title: "Description"
                        },
                        {
                            template: "<button type='button' onclick='DeleteRow($(this))'>Delete</button>"
                        }
                    ],
                    dataSource: new kendo.data.DataSource({
                        data: data == null ? [] : data.Pictures,
                        schema: {
                            model: {
                                id: "Id",
                                fields: {
                                    Id: { editable: false },
                                    Photo: { editable: false },
                                    Description: { editable: true, validation: { required: true, min:1 } }

                                },
                            }
                        }
                    })
                });

                $("#gridPicturesPOI").kendoTooltip({
                    autoHide: true,
                    showOn: "click",
                    width: 100,
                    height: 100,
                    position: "top",
                    filter: ".k-grid-content div.hasTooltip",
                    content: function (e) {
                        var template = kendo.template($("#storeTerritory").html());
                        return template({ Photo: $(e.target).children('img').attr('src') });
                    }
                });



                $("#btn_POI_Cancel").kendoButton({
                    click: function () {
                        $("#windowPOI").data("kendoWindow").close();
                    }
                });
                $("#btn_Save_POI").kendoButton({
                    click: function () {
                        var gridSuspects = $("#gridSuspects").data("kendoGrid");
                        var validatable = $("#gridPicturesPOI").data().kendoGrid.dataSource.view();
                        for (var i = 0; i < validatable.length; i++) {
                            var desc = validatable[i].Description;
                        }
                        if (validatable.length == 0)
                        {
                            desc = -1;
                        }
                        if ($("#txtPOILastName").val() != "" && $("#txtPOIFirstName").val() != "" && $("#comboPOIGender").data("kendoDropDownList").value() != "" && $("#comboPOIType").data("kendoDropDownList").value() != "" && (desc!= undefined || desc == -1))
                        {
                            if (data == null) {
                                gridSuspects.dataSource.add({
                                    "Id": 0,
                                    "Person": {
                                        "First_Name": $("#txtPOIFirstName").val(),
                                        "Last_Name": $("#txtPOILastName").val(),
                                        "Middle_Name": $("#txtPOIMiddleName").val(),
                                        "DOB": $("#dtPOIDOB").val(),
                                        "Street": $("#txtPOIStreet").val(),
                                        "City": $("#txtPOICity").val(),
                                        "State": $("#txtPOIState").val(),
                                        "Zip": $("#txtPOIZip").val(),
                                        "Home_Number": $("#txtPOIHome").val(),
                                        "Other_Number": $("#txtPOIOther").val(),
                                        "Mobile_Number": $("#txtPOIMobile").val(),
                                        "AKA": $("#txtPOIAKA").val(),
                                        "Gender":
                                        {
                                            "Id": $("#comboPOIGender").data("kendoDropDownList").value()
                                        }
                                    },
                                    "Type":
                                    {
                                        "Id": $("#comboPOIType").data("kendoDropDownList").value(),
                                        "Description": $("#comboPOIType").data("kendoDropDownList").text()
                                    },
                                    "Comments": $("#txtComments").val(),
                                    "Pictures": $("#gridPicturesPOI").data().kendoGrid.dataSource.view()
                                });
                            }
                            else {
                                data.Comments = $("#txtComments").val();
                                data.Type.Id = $("#comboPOIType").data("kendoDropDownList").value();
                                data.Type.Description = $("#comboPOIType").data("kendoDropDownList").text();
                                data.Person.First_Name = $("#txtPOIFirstName").val();
                                data.Person.Last_Name = $("#txtPOILastName").val();
                                data.Person.Middle_Name = $("#txtPOIMiddleName").val();
                                data.Person.dob = $("#dtPOIDOB").val();
                                data.Person.Street = $("#txtPOIStreet").val();
                                data.Person.City = $("#txtPOICity").val();
                                data.Person.State = $("#txtPOIState").val();
                                data.Person.Zip = $("#txtPOIZip").val();
                                data.Person.Home_Number = $("#txtPOIHome").val();
                                data.Person.Other_Number = $("#txtPOIOther").val();
                                data.Person.Mobile_Number = $("#txtPOIMobile").val();
                                data.Person.Aka = $("#txtPOIAKA").val();
                                data.Person.Gender.Id = $("#comboPOIGender").data("kendoDropDownList").value();
                                data.Pictures = $("#gridPicturesPOI").data().kendoGrid.dataSource.view();
                                //dataItem = gridSuspects.dataItem(gridSuspects.select());
                                //dataItem.set("Comments", $("#txtComments").val());
                                //dataItem.set("Type.Id", $("#comboPOIType").data("kendoDropDownList").value());
                                //dataItem.set("Type.Description", $("#comboPOIType").data("kendoDropDownList").text());                           
                                //dataItem.set("Person.First_Name", $("#txtPOIFirstName").val());
                                //dataItem.set("Person.Last_Name", $("#txtPOILastName").val());
                                //dataItem.set("Person.Middle_Name", $("#txtPOIMiddleName").val());
                                //dataItem.set("Person.DOB", $("#dtPOIDOB").val());
                                //dataItem.set("Person.Street", $("#txtPOIStreet").val());
                                //dataItem.set("Person.City", $("#txtPOICity").val());
                                //dataItem.set("Person.State", $("#txtPOIState").val());
                                //dataItem.set("Person.Zip", $("#txtPOIZip").val());
                                //dataItem.set("Person.Home_Number", $("#txtPOIHome").val());
                                //dataItem.set("Person.Other_Number", $("#txtPOIOther").val());
                                //dataItem.set("Person.Mobile_Name", $("#txtPOIMobile").val());
                                //dataItem.set("Person.AKA", $("#txtPOIAKA").val());
                                //dataItem.set("Person.Gender.Id", $("#comboPOIGender").data("kendoDropDownList").value());
                            }
                            $("#windowPOI").data("kendoWindow").close();
                            return false;

                        }
                        else {
                            if ($("#comboPOIType").data("kendoDropDownList").value() == "")
                            { alert("POI Type is Required"); return false; }
                            if ($("#txtPOILastName").val() == "")
                            { alert("Last Name is Required"); return false; }
                            if ($("#txtPOIFirstName").val() == "")
                            {
                                alert("First Name is Required");
                                return false;
                            }
                            if ($("#comboPOIGender").data("kendoDropDownList").value() == "")
                            { alert("Gender is Required"); return false; }
                                                                            
                            if (desc == undefined) {
                                alert("Photo Description is Required");
                                return false;
                            }
                        }                    
                    }
                });

            },
            actions: [
                //"Pin",
                //"Minimize",
                //"Maximize",
                "Close"
            ]
        }).data("kendoWindow");

        var template = kendo.template($("#POI").html());
        myPOIWindow.data("kendoWindow")

            .content(template(data ? data : {}))
            .center().open();

        myPOIWindow
            .find(".password-ok")
            .click(function () {
                kendoWindow.data("kendoWindow").close();
            })
            .end()
    }
});
