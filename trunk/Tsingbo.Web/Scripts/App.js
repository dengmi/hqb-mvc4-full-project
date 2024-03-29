﻿function editor(obj) {
    var editor;
    KindEditor.ready(function (K) {
        editor = K.create(obj, {
            themesPath: "/Content/plugins/editor/",
            themeType: 'default',
            resizeType: 1,
            allowPreviewEmoticons: false,
            allowImageUpload: false,
            items: ['fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', 'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist', 'insertunorderedlist', '|', 'link']
        });
    });
}


$(function () {

    $(".editor").each(function () {
        editor(this);
    });
    // Datepicker
    $(":input[data-datepicker=true]").datepicker({
        numberOfMonths: 1,
        dateFormat: 'yy/mm/dd',
        constrainInput: true,
        showButtonPanel: true,
        stepMonths: 1,
        //minDate: "0D",
        maxDate: "+331D",
        defaultDate: new Date(),
        buttonImageOnly: false,
        inline: true
    });
    // Accordion
    $(".accordion").accordion({
        header: "h3"
    });
    // Buttons
    $('button').button();
    // Dialog Link
    $('.dialog_link').click(function () {
        $('#dialog').dialog('open');
        return false;
    });
    // Modal Link
    $('.modal_link').click(function () {
        $('#dialog-message').dialog('open');
        return false;
    });
    // Dialogs
    $("#dialog-message").dialog({
        width: 350,
        autoOpen: false,
        modal: true,
        buttons: {
            Ok: function () {
                //$(this).dialog("close");
                alert("ok");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });

    $('#radioset').buttonset();


    //hover states on the static widgets
    $('#dialog_link, #modal_link, ul.icons li').hover(
    function () {
        $(this).addClass('ui-state-hover');
    }, function () {
        $(this).removeClass('ui-state-hover');
    });
});

//#region 树形菜单
$(function () {
    $.fn.extend({
        SimpleTree: function (options) {
            //初始化参数
            var option = $.extend({
                click: function (a) { }
            }, options);
            option.tree = this;	/* 在参数对象中添加对当前菜单树的引用，以便在对象中使用该菜单树 */
            option._init = function () {
                /*
				 * 初始化菜单展开状态，以及分叉节点的样式
				 */
                this.tree.find("ul ul").hide();	/* 隐藏所有子级菜单 */
                this.tree.find("ul ul").prev("li").removeClass("open");	/* 移除所有子级菜单父节点的 open 样式 */
                this.tree.find("ul ul[show='true']").show();	/* 显示 show 属性为 true 的子级菜单 */
                this.tree.find("ul ul[show='true']").prev("li").addClass("open");	/* 添加 show 属性为 true 的子级菜单父节点的 open 样式 */
            }/* option._init() End */
            /* 设置所有超链接不响应单击事件 */
            this.find("a").click(function () { $(this).parents("li").click(); return false; });
            /* 菜单项 <li> 接受单击 */
            this.find("li").click(function () {
                /*
				 * 当单击菜单项 <li>
				 * 1.触发用户自定义的单击事件，将该 <li> 标签中的第一个超链接做为参数传递过去
				 * 2.修改当前菜单项所属的子菜单的显示状态（如果等于 true 将其设置为 false，否则将其设置为 true）
				 * 3.重新初始化菜单
				 */
                var a = $(this).find("a")[0];
                if (typeof (a) != "undefined") {
                    option.click(a);	/* 如果获取的超链接不是 undefined，则触发单击 */
                }
                /* 
				 * 如果当前节点下面包含子菜单，并且其 show 属性的值为 true，则修改其 show 属性为 false
				 * 否则修改其 show 属性为 true
				 */
                if ($(this).next("ul").attr("show") == "true") {
                    $(this).next("ul").attr("show", "false");
                } else {
                    $(this).next("ul").attr("show", "true");
                }
                /* 初始化菜单 */
                option._init();
            });

            this.find("li").hover(
				function () {
				    $(this).addClass("hover");
				},
				function () {
				    $(this).removeClass("hover");
				}
			);

            /* 设置所有父节点样式 */
            this.find("ul").prev("li").addClass("folder");
            /* 设置节点“是否包含子节点”属性 */
            this.find("li").find("a").attr("hasChild", false);
            this.find("ul").prev("li").find("a").attr("hasChild", true);
            /* 初始化菜单 */
            option._init();
        }/* SimpleTree Function End */
    });
});
//#endregion