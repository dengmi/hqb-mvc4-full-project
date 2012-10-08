$(document).ready(function () {
    SetWindowSize();
    $(window).resize(function () {
        SetWindowSize();
    });
});
function SetWindowSize() {
    var body = $(window).height();
    var header = $('#header').height();
    var footer = $('#footer').height();
    $('#main-container').height(body - header - footer);
    $('#main-container').omBorderLayout({
        panels: [{
            id: "main",
            header: false,
            region: "center"
        }, {
            id: "sidebar",
            resizable: true,
            collapsible: true,
            title: "导航",
            region: "west", width: 180
        }]
    });

    var tabElement = $('#center-tab').omTabs({
        height: "fit"
    });
    var navData = [{ id: "n1", text: "搜索引擎", expanded: true },
             { id: "n2", text: "中间件", expanded: true },
             { id: "n11", pid: "n1", text: "百度", url: "http://www.baidu.com" },
             { id: "n12", pid: "n2", text: "金蝶中间件", url: "http://www.apusic.com/homepage/index.faces" }
    ];

    $("#navTree").omTree({
        dataSource: navData,
        simpleDataModel: true,
        onClick: function (nodeData, event) {
            if (nodeData.url) {
                var tabId = tabElement.omTabs('getAlter', 'tab_' + nodeData.id);
                if (tabId) {
                    tabElement.omTabs('activate', tabId);
                } else {
                    tabElement.omTabs('add', {
                        title: nodeData.text,
                        tabId: 'tab_' + nodeData.id,
                        content: "<iframe id='" + nodeData.id + "' border=0 frameBorder='no' name='inner-frame' src='" + nodeData.url + "' height='" + ifh + "' width='100%'></iframe>",
                        closable: true
                    });
                }
            }
        }
    });
    var ifh = tabElement.height() - tabElement.find(".om-tabs-headers").outerHeight() - 4; //为了照顾apusic皮肤，apusic没有2px的padding，只有边框，所以多减去2px

    //    $('#3Dbox').height(ifh);
}