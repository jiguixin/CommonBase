function ChangeToTable(printDatagrid) {
    var tableString = '<table cellspacing="0" class="pb">';
    var frozenColumns = printDatagrid.datagrid("options").frozenColumns;  // 得到frozenColumns对象
    var columns = printDatagrid.datagrid("options").columns;    // 得到columns对象
    var nameList = new Array();

    // 载入title
    if (typeof columns != 'undefined' && columns != '') {
        $(columns).each(function (index) {
            tableString += '\n<tr>';
            if (typeof frozenColumns != 'undefined' && typeof frozenColumns[index] != 'undefined') {
                for (var i = 0; i < frozenColumns[index].length; ++i) {
                    if (!frozenColumns[index][i].hidden && frozenColumns[index][i].field != "_expander") {
                        tableString += '\n<th width="' + frozenColumns[index][i].width + '"';
                        if (typeof frozenColumns[index][i].rowspan != 'undefined' && frozenColumns[index][i].rowspan > 1) {
                            tableString += ' rowspan="' + frozenColumns[index][i].rowspan + '"';
                        }
                        if (typeof frozenColumns[index][i].colspan != 'undefined' && frozenColumns[index][i].colspan > 1) {
                            tableString += ' colspan="' + frozenColumns[index][i].colspan + '"';
                        }
                        if (typeof frozenColumns[index][i].field != 'undefined' && frozenColumns[index][i].field != '') {
                            nameList.push(frozenColumns[index][i]);
                        }
                        tableString += '>' + frozenColumns[0][i].title + '</th>';
                    }
                }
            }
            for (var i = 0; i < columns[index].length; ++i) {
                if (!columns[index][i].hidden) {
                    tableString += '\n<th width="' + columns[index][i].width + '"';
                    if (typeof columns[index][i].rowspan != 'undefined' && columns[index][i].rowspan > 1) {
                        tableString += ' rowspan="' + columns[index][i].rowspan + '"';
                    }
                    if (typeof columns[index][i].colspan != 'undefined' && columns[index][i].colspan > 1) {
                        tableString += ' colspan="' + columns[index][i].colspan + '"';
                    }
                    if (typeof columns[index][i].field != 'undefined' && columns[index][i].field != '') {
                        nameList.push(columns[index][i]);
                    }
                    tableString += '>' + columns[index][i].title + '</th>';
                }
            }
            tableString += '\n</tr>';
        });
    }
    // 载入内容
    var rows = printDatagrid.datagrid("getRows"); // 这段代码是获取当前页的所有行
    for (var i = 0; i < rows.length; ++i) {
        tableString += '\n<tr>';
        for (var j = 0; j < nameList.length; ++j) {
            var e = nameList[j].field.lastIndexOf('_0');

            tableString += '\n<td';
            if (nameList[j].align != undefined && nameList[j].align != '') {
                tableString += ' style="text-align:' + nameList[j].align + ';"';
            }
            tableString += '>';
            if (e + 2 == nameList[j].field.length) {
                var value = rows[i][nameList[j].field.substring(0, e)];
                if (value != null) {
                    if (!isNaN(value) && value.length > 11) {
                        value = "'" + value;
                    }
                    value = value.toString();
                    tableString += value;
                }
                
            } else {
                var value1 = rows[i][nameList[j].field];
                if (value1 != null) {
                    if (!isNaN(value1) && value1.length > 11) {
                        value1 = "'" + value1;
                    }
                    value1 = value1.toString();
                    tableString += value1;

                }
               
            }
           
            tableString += '</td>';
        }
        tableString += '\n</tr>';
    }
    tableString += '\n</table>';
    return tableString;
}
function exportToExcel(jq,strXlsName) {
    var f = $('<form action="/RestApi/ExportToExcel" method="post" id="fm1"></form>');
    var i = $('<input type="hidden" id="txtContent" name="txtContent" />');
    var l = $('<input type="hidden" id="txtName" name="txtName" />');
    i.val(ChangeToTable(jq));
    i.appendTo(f);
    l.val(strXlsName);
    l.appendTo(f);
    f.appendTo(document.body).submit();
    document.body.removeChild(f);
}
$.extend($.fn.datagrid.methods, {
    addToolbarItem: function (jq, items) {
        return jq.each(function () {
            var dpanel = $(this).datagrid('getPanel');
            var toolbar = dpanel.children("div.datagrid-toolbar");
            if (!toolbar.length) {
                toolbar = $("<div class=\"datagrid-toolbar\"><table cellspacing=\"0\" cellpadding=\"0\"><tr></tr></table></div>").prependTo(dpanel);
                $(this).datagrid('resize');
            }
            var tr = toolbar.find("tr");
            for (var i = 0; i < items.length; i++) {
                var btn = items[i];
                if (btn == "-") {
                    $("<td><div class=\"datagrid-btn-separator\"></div></td>").appendTo(tr);
                } else {
                    var td = $("<td></td>").appendTo(tr);
                    var b = $("<a href=\"javascript:void(0)\"></a>").appendTo(td);
                    b[0].onclick = eval(btn.handler || function () { });
                    b.linkbutton($.extend({}, btn, {
                        plain: true
                    }));
                }
            }
        });
    },
    removeToolbarItem: function (jq, param) {
        return jq.each(function () {
            var dpanel = $(this).datagrid('getPanel');
            var toolbar = dpanel.children("div.datagrid-toolbar");
            var cbtn = null;
            if (typeof param == "number") {
                cbtn = toolbar.find("td").eq(param).find('span.l-btn-text');
            } else if (typeof param == "string") {
                cbtn = toolbar.find("span.l-btn-text:contains('" + param + "')");
            }
            if (cbtn && cbtn.length > 0) {
                cbtn.closest('td').remove();
                cbtn = null;
            }
        });
    },
    removeAllToolbar: function(jq) {
        return jq.each(function() {
            var dpanel = $(this).datagrid('getPanel');
            var toolbar = dpanel.children("div.datagrid-toolbar");
            var tr = toolbar.find("tr");
            tr.empty();
        });
    }
});
/** 
* 扩展表格列对齐属性： 
*      自定义一个列字段属性： 
*      headalign ：原始align属性针对数据有效, headalign针对列名有效
*      
*/
$.extend($.fn.datagrid.defaults, {
    onLoadSuccess: function () {
        var target = $(this);
        var opts = $.data(this, "datagrid").options;
        var panel = $(this).datagrid("getPanel");
        //获取列
        var fields = $(this).datagrid('getColumnFields', false);
        //datagrid头部 table 的第一个tr 的td们，即columns的集合
        var headerTds = panel.find(".datagrid-view2 .datagrid-header .datagrid-header-inner table tr:first-child").children();
        //重新设置列表头的对齐方式
        headerTds.each(function(i, obj) {
            var col = target.datagrid('getColumnOption', fields[i]);
            if (!col.hidden && !col.checkbox) {
                var headalign = col.headalign || col.align || 'left';
                $("div:first-child", obj).css("text-align", headalign);
            }
        });
    }
});