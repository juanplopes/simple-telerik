<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:content contentPlaceHolderID="MainContent" runat="server">

<script src="http://static.ak.connect.facebook.com/js/api_lib/v0.4/FeatureLoader.js.php" type="text/javascript"></script>

<div class="fb-background">
    <div class="introduction">
        <h4>Binding to External Web Service: Facebook</h4>
        <p>
            To view this example, you need to connect with a <strong>Facebook application</strong>,
            which gives us access to your <strong>name and friends list</strong>.
        </p>
        <p>
            <strong>We won't collect your information, nor write on your Wall!</strong><br />
            If you don't take our word for it, you can check the source code below.<br />
        </p>
        <p class="button-area">
            <a href="#" class="fb-connect">Connect to Facebook</a>
        </p>
    
        <div class="loading"></div>
    </div>

    <div class="profile">
        <div id="profile-info">
            Welcome, <span id="fb-name"></span>
            <img src="<%= ResolveUrl("~/Content/Grid/ExternalServiceFacebook/fb_silhouette.png") %>"
                 id="fb-pic" width="50" height="50" />
        </div>

        <%= Html.Telerik().Grid<object>()
                .Name("friend-list")
                .Columns(columns =>
                {
                    columns.Template(o => { }).Title("Picture").Width(65);
                    columns.Template(o => { }).Title("Name");
                })
                .ClientEvents(events => events
                    .OnRowDataBound("onRowDataBound")
                    .OnDataBinding("onDataBinding")
                )
                .Pageable()
        %>
    </div>
</div>

<p class="disclaimer">Facebook is a registered trademark of Facebook, Inc.</p>

<script type="text/javascript">
    var api_key = 'd04fba62ff27c6c84a6b767d404bcec3';
    var channel_path = '<%= ResolveUrl("~/Content/Grid/ExternalServiceFacebook/xd_receiver.htm") %>';
    var currentUserId = -1;
    
    function onDataBinding(e) {
    
        // discard databinding prior to intialization
        if (currentUserId == -1)
            return;
        
        var grid = $('#friend-list').data('tGrid');
        var api = FB.Facebook.apiClient;
        
        api.callMethod('Fql.multiquery', { queries: {
                friends: [
                    'SELECT uid2 FROM friend ',
                    ' WHERE uid1 = ', currentUserId
                ].join(''),
                friends_info: [
                    'SELECT name, pic_square FROM user ',
                    'WHERE uid IN (SELECT uid2 FROM #friends) ',
                    'LIMIT ', (e.page - 1) * 10, ', ', e.page * 10 /* sugary MySQL paging */
                ].join('')
            }},
            function(multiQueryResult) {
                // necessary for first pager update,
                //  but can be set each time (in case friends are added)
                grid.total = multiQueryResult[0].fql_result_set.length;
                // show the data in the grid
                grid.dataBind(multiQueryResult[1].fql_result_set);
                
                $('.introduction').hide();
                $('.profile').show();
            });
    }
    
    function onRowDataBound(e) {
        var row = e.row;
        var dataItem = e.dataItem;
        
        row.cells[0].innerHTML = 
                        ['<img src="',
                         dataItem['pic_square']
                            || '<%= ResolveUrl("~/Content/Grid/ExternalServiceFacebook/fb_silhouette.png") %>',
                         '" width="50" height="50" />'].join('');
        row.cells[1].innerHTML = dataItem['name'];
    }
    
    function showFacebookInfo() {
        $('.loading').show();
    
        var api = FB.Facebook.apiClient;
        
        // require user login
        api.requireLogin(function(exception){
            api.users_getLoggedInUser(function(currentUserId, exception) {
                window.currentUserId = currentUserId;
                
                // query profile info
                api.users_getInfo(currentUserId, ['name', 'pic_square'], function(result) {
                    $('#fb-name').text(result[0]['name']);
                    
                    if (result[0]['pic_square'])
                        $('#fb-pic').attr('src', result[0]['pic_square']);
                });
                
                // show friends list, page 1
                onDataBinding({ page: 1 });
            });
        });
    }
</script>

<div id="loading-overlay"></div>
<%
    Html.Telerik().ScriptRegistrar()
        .OnDocumentReady(() =>
        {%>
        
        $('.fb-connect').click(function(e) { 
            e.preventDefault();
            
            alert("Facebook API not yet initialized. Please wait a moment.");
        });
        
        /* initialize facebook API */
        FB_RequireFeatures(['Api'], function() {
            FB.Facebook.init(api_key, channel_path);
            
            if (/session=/.test(window.location.href))
                showFacebookInfo();
            else
                $('.fb-connect').unbind('click').click(function(e) { e.preventDefault(); showFacebookInfo(); });
        });
    <%});
%>

</asp:content>

<asp:content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .fb-background
        {
            background: #cfd6e6 url('<%= ResolveUrl("~/Content/Grid/ExternalServiceFacebook/background.png") %>') no-repeat 0 0;
            border-bottom: 7px solid #3b5998;
            padding-top: 53px;
            color: #3b5998;
            font-size: 14px;
            line-height: 1.3;
            width: 568px;
            margin: 0 auto;
        }
        
        .introduction
        {
            padding: 40px 50px 35px;
            position: relative;
        }
        
        .button-area
        {
            text-align: right;
            padding-top: 30px;
        }
        
        .fb-connect
        {
            background-image: url('<%= ResolveUrl("~/Content/Grid/ExternalServiceFacebook/fb_connect.png") %>');
            width: 96px;
            height: 25px;
            text-indent: 9999px;
            white-space: nowrap;
            overflow: hidden;
            outline: none;
            display: inline-block;
            *display: inline;
            zoom: 1;
        }
        
        .loading
        {
            position: absolute;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            opacity: .5;
            display: none;
            background: #fff url('<%= ResolveUrl("~/Content/Grid/ExternalServiceFacebook/loading.gif") %>') no-repeat 50% 50%;
        }
        
        .profile
        {
            display: none;
        }
        
        #profile-info
        {
            height: 70px;
            position: relative;
            padding: 32px 0 0 93px;
        }
        #fb-name { display: block; font-size: 18px; font-weight: bold; }
        #fb-pic { position: absolute; top: 27px; left: 29px; }
        
        .disclaimer
        {
            font-style: italic;
            width: 568px;
            text-align: center;
            margin: 20px auto 40px;
        }
    </style>
</asp:content>
