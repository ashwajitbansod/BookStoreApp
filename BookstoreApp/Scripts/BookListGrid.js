
var base_url = window.location.origin;
(function ($) {
    'use strict'
    var data;
    $("#GetBookList").jsGrid({
        width: "100%",
        height: "400px",
        filtering: true,
        inserting: true,
        editing: true,
        sorting: true,
        paging: true,
        autoload: true,
        pageSize: 10,
        pageButtonCount: 5,
        loadMessage: "Please, wait...",
        controller: {
            loadData: function (filter) {
                return $.ajax({
                    type: "GET",
                    url: base_url + "/BookStore/GetBookListData",
                    datatype: 'json',
                    contentType: "application/json",
                });
            }
        },
        onRefreshed: function (args) {
            $(".jsgrid-insert-row").hide();
            $(".jsgrid-filter-row").hide()
            $(".jsgrid-grid-header").removeClass("jsgrid-header-scrollbar");

        },
        fields: [
            {
                name: "BookPic", title: "Book Image",
                itemTemplate: function (val, item) {
                    return $("<img>").attr("src", val).css({ height: 50, width: 50 }).on("click", function () {

                    });
                }
            },
            {name: 'BookName',  title: "Book Name"},
            { name: 'AuthorName',  title: "Author" },
            { name: 'BookPublication',  title: "Publication" },
            {
                        name: "Action", title: "Action", itemTemplate: function (value, item) {
                    var $iconView = $("<span>").append('<i class= "fa fa-eye fa-2x" style="color:black;margin-left: 11px;margin-top: 12px;" ></i>');// $("<span>").attr({ class: "fa fa-folder-open fa-2x" }).attr({ style: "color:yellow;background-color:green;margin-left:20px;border-radius:35px;width:35px;height:35px" });
                    var $iconEdit = $("<span>").append('<i class= "fa fa-pencil fa-2x" style="color:orange;margin-left: 11px;margin-top: 12px;" ></i>');//attr({ class: "fa fa-user fa-2x" }).attr({ style: "color:white;background-color:#36CA7E;margin-left:20px;border-radius:35px;width:35px;height:35px" });
                    var $iconDelete = $("<span>").append('<i class= "fa fa-trash fa-2x" style="color:red;margin-left: 11px;margin-top: 12px;"></i>');//attr({ class: "fa fa-file fa-2x" }).attr({ style: "color:white;background-color:#D26C36;margin-left:20px;border-radius:35px;width:35px;height:35px" });
                            
                    var $customViewButton = $("<span>")
                        .attr({ title: "View Details" })
                        .attr({ id: "btn-file-" + item.BookId }).click(function (e) {
                            debugger
                            $.ajax({
                                type: "POST",
                                url: base_url + '/BookStore/GetBookDataById?BookId=' + item.BookId,
                                contentType: "application/json; charset=utf-8",
                                error: function (xhr, status, error) {
                                },
                                success: function (result) {
                                    debugger
                                    $("#DisplayData").html("");
                                    $("#DisplayData").html(result);
                                    $("#myModalForDisplayBookDetails").modal('show');
                                }
                            });
                            e.stopPropagation();
                        }).append($iconView);

                    var $customEditButton = $("<span>")
                        .attr({ title: "Edit Book" })
                        .attr({ id: "btn-file-" + item.BookId }).click(function (e) {
                            debugger
                            window.location.href = base_url + '/BookStore/GetDetailsForEdit?BookId=' + item.BookId;
                            e.stopPropagation();
                        }).append($iconEdit);
                    var $customDeleteButton = $("<span>")
                        .attr({ title: "Delete Book" })
                        .attr({ id: "btn-file-" + item.BookId }).click(function (e) {
                            debugger
                            $.ajax({
                                type: "POST",
                                url: base_url + '/BookStore/DeleteBookData?BookId=' + item.BookId,
                                contentType: "application/json; charset=utf-8",
                                error: function (xhr, status, error) {
                                },
                                success: function (result) {
                                    debugger
                                    alert("Book deleted successfuly.")
                                    $("#GetBookList").jsGrid("loadData");
                                },

                            });
                            e.stopPropagation();
                        }).append($iconDelete);

                    return $("<div>").attr({ class: "btn-toolbar" }).append($customViewButton).append($customEditButton).append($customDeleteButton);
                        }
                    }
        ],
        rowClick: function (args) {
            this
            console.log(args)
            var getData = args.item;
            var keys = Object.keys(getData);
            var text = [];
        }
    });
})(jQuery);