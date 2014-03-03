//根据页面获取按钮权限
function addToolBar() {
    removeToolBar();

    //首先获取iframe标签的id值
    var iframeid = window.parent.$('#tabs').tabs('getSelected').find('iframe').attr("id");

    //获取按钮值
    $.post('/RestApi/GetCurrentUserButtonsPrivilege?menuId='+ iframeid, function (data) {
        if (data == null) {
            return;
        }
        $('#fixedGrid').datagrid("addToolbarItem", data);
    });
}
//删除ToolBar所有的按钮。
function removeToolBar() {
    /*//首先获取iframe标签的id值
    var iframeid = window.parent.$('#tabs').tabs('getSelected').find('iframe').attr("id");

    //获取按钮值
    $.post('/RestApi/GetCurrentUserButtonsPrivilege?menuId=' + iframeid, function (data) {
        if (data == null) {
            return;
        }
        try {
            for (var i = 0; i < data.length; i++) {
                $('#fixedGrid').datagrid("removeToolbarItem", data[i]);
            }
        } catch(e) {
            alert(e.message);
        } 
        
    });*/
    $('#fixedGrid').datagrid("removeAllToolbar");
}

function pagerFilter(data) {
    if (typeof data.length == 'number' && typeof data.splice == 'function') {    // is array
        data = {
            total: data.length,
            rows: data
        };
    }
    var dg = $(this);
    var opts = dg.datagrid('options');
    var pager = dg.datagrid('getPager');
    pager.pagination({
        onSelectPage: function (pageNum, pageSize) {
            opts.pageNumber = pageNum;
            opts.pageSize = pageSize;
            pager.pagination('refresh', {
                pageNumber: pageNum,
                pageSize: pageSize
            });
            dg.datagrid('loadData', data);
        }
    });
    if (!data.originalRows) {
        data.originalRows = (data.rows);
    }
    var start = (opts.pageNumber - 1) * parseInt(opts.pageSize);
    var end = start + parseInt(opts.pageSize);
    data.rows = (data.originalRows.slice(start, end));
    return data;
}

//格式化字符串
String.prototype.format = function (args) {
    var result = this;
    if (arguments.length > 0) {
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                if (args[key] != undefined) {
                    var reg = new RegExp("({" + key + "})", "g");
                    result = result.replace(reg, args[key]);
                }
            }
        } else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] != undefined) {
                    //var reg = new RegExp("({[" + i + "]})", "g");//这个在索引大于9时会有问题  
                    var reg = new RegExp("({)" + i + "(})", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
    }
    return result;
};