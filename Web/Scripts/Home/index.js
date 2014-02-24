
var _menus = [];

$(function () {
    InitLeftMenu();
    tabClose();
    tabCloseEven();


    $('#tabs').tabs({
        onSelect: function (title) {
            var currTab = $('#tabs').tabs('getTab', title);
            var iframe = $(currTab.panel('options').content);

            var src = iframe.attr('src');
            var menuId = iframe.attr('id');
            if (src)
                $('#tabs').tabs('update', { tab: currTab, options: { content: createFrame(src, menuId) } });
        }
    });

});


//初始化左侧
function InitLeftMenu() {
    $("#nav").accordion({ animate: false });


    $.getJSON(getCurrentUserMenu + "?" + new Date().getTime(), function (data) {
        _menus = data;
        
        var menulist = '';
        $.each(data, function(i, menu) {
            if (menu.checked && menu.visible == '启用') {
                menulist = '<ul>';
                $.each(menu.children, function (n, child) {
                    if (child.checked && child.visible == '启用') {
                        menulist += '<li><div><a ref="' + child.order + '" href="#"  rev="' + child.id + '"  rel="' + child.link + '" ><span class="icon ' + child.iconCls + '" >&nbsp;</span><span class="nav">' + child.text + '</span></a></div></li> ';
                    }
                });
                menulist += '</ul>';
                $('#nav').accordion('add', {
                    title: menu.text,
                    content: menulist,
                    iconCls: menu.iconCls
                });
            }
        });
        //原来的加载可用菜单方法，会遇到父菜单禁用子菜单依然显示和排序混乱的bug
        //$.each(data, function (i, n) {
        //    //如果是父节点,则添加<ul>标示
        //    if (n.MenuParentId == null && n.isCheck == true) {
        //        //如果上一次的父节点名称不为空，表示已经menulist已经有一组菜单需要添加
        //        //如果上一次的父节点名称为空，表示是第一次获取到父节点要素，menulist还未生成
        //        if (menuListName != null) {
        //            $('#nav').accordion('add', {
        //                title: menuListName += '</ul>',
        //                content: menulist,
        //                iconCls: 'icon ' + n.MenuIcon
        //            });

        //        }
        //        menulist = '<ul>';
        //        menuListName = n.MenuName;

        //    } else if (n.isCheck == true&&n.IsVisible==1) {
        //        menulist += '<li><div><a ref="' + n.MenuOrder + '" href="#"  rev="' + n.SysId + '"  rel="' + n.MenuLink + '" ><span class="icon ' + n.MenuIcon + '" >&nbsp;</span><span class="nav">' + n.MenuName + '</span></a></div></li> ';
        //    }

        //    //循环结束时候再添加一次menulist，否则最后一组的menulist不会显示
        //    if (i == data.length - 1) {
        //        menulist += '</ul>';
        //        $('#nav').accordion('add', {
        //            title: menuListName,
        //            content: menulist,
        //            iconCls: 'icon ' + n.MenuIcon
        //        });
        //    }
        //});
        
        //实现子菜单点击链接
        $('.easyui-accordion li a').click(function () {
            var tabTitle = $(this).children('.nav').text();

            var url = $(this).attr("rel");
            var menuid = $(this).attr("rev");
            var icon = getIcon(menuid);

            addTab(tabTitle, url, icon, menuid);
            $('.easyui-accordion li div').removeClass("selected");
            $(this).parent().addClass("selected");
        }).hover(function () {
            $(this).parent().addClass("hover");
        }, function () {
            $(this).parent().removeClass("hover");
        });

        //选中第一个
        var panels = $('#nav').accordion('panels');
        if (panels.length>0) {
            var t = panels[0].panel('options').title;
            $('#nav').accordion('select', t);
        }
        
    });

     
    $.getJSON(getCurrentUserInfoUrl + "?" + new Date().getTime(), function (data) {
        $("#userName")[0].textContent = data.UserInfo.RealName;
        //$("#userName")[0].innerHtml在火狐下不能修改内容
    });
    

}
//获取左侧导航的图标
function getIcon(menuid) {
    var icon = 'icon ';
    $.each(_menus, function (i, n) {
        if (n.SysId == menuid) {
            icon += n.MenuIcon;
        }
    });

    return icon;
}

function addTab(subtitle, url, icon, menuId) {
    if (!$('#tabs').tabs('exists', subtitle)) {
        $('#tabs').tabs('add', {
            title: subtitle,
            content: createFrame(url, menuId),
            closable: true,
            icon: icon
        });
    } else {
        $('#tabs').tabs('select', subtitle);
        $('#mm-tabupdate').click();
    }
    tabClose();
}

function createFrame(url, menuId) {
    var s = '<iframe frameborder="0" id="'+menuId+'" scrolling="no" src="' + url + '" style="width:100%;height:99.5%;"></iframe>';
    return s;
}

function tabClose() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children(".tabs-closable").text();
        $('#tabs').tabs('close', subtitle);
    });
    /*为选项卡绑定右键*/
    $(".tabs-inner").bind('contextmenu', function (e) {
        $('#mm').menu('show', {
            left: e.pageX,
            top: e.pageY
        });

        var subtitle = $(this).children(".tabs-closable").text();

        $('#mm').data("currtab", subtitle);
        $('#tabs').tabs('select', subtitle);
        return false;
    });
}
//绑定右键菜单事件
function tabCloseEven() {
    //刷新
    $('#mm-tabupdate').click(function () {
        var currTab = $('#tabs').tabs('getSelected');
        var url = $(currTab.panel('options').content).attr('src');
        var menuId = $(currTab.panel('options').content).attr('id');
        $('#tabs').tabs('update', {
            tab: currTab,
            options: {
                content: createFrame(url,menuId)
            }
        });
    });
    //关闭当前
    $('#mm-tabclose').click(function () {
        var currtab_title = $('#mm').data("currtab");
        $('#tabs').tabs('close', currtab_title);
    });
    //全部关闭
    $('#mm-tabcloseall').click(function () {
        $('.tabs-inner span').each(function (i, n) {
            var t = $(n).text();
            $('#tabs').tabs('close', t);
        });
    });
    //关闭除当前之外的TAB
    $('#mm-tabcloseother').click(function () {
        $('#mm-tabcloseright').click();
        $('#mm-tabcloseleft').click();
    });
    //关闭当前右侧的TAB
    $('#mm-tabcloseright').click(function () {
        var nextall = $('.tabs-selected').nextAll();
        if (nextall.length == 0) {
            //msgShow('系统提示','后边没有啦~~','error');
            alert('后边没有啦~~');
            return false;
        }
        nextall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });
    //关闭当前左侧的TAB
    $('#mm-tabcloseleft').click(function () {
        var prevall = $('.tabs-selected').prevAll();
        if (prevall.length == 0) {
            alert('到头了，前边没有啦~~');
            return false;
        }
        prevall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });

    //退出
    $("#mm-exit").click(function () {
        $('#mm').menu('hide');
    })
}

//弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType) {
    $.messager.alert(title, msgString, msgType);
}
