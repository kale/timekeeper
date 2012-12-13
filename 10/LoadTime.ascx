<%@ Control Language="C#" ClassName="LoadTime" %>
<%@ Import Namespace = "smartduck" %>

<script runat="server">
	protected StopWatch timer = new StopWatch();
	protected string s = "";

	protected void LoadTimeLabel_Init(object sender, EventArgs e)
	{
		timer.Start();
		s = "PreInit";
	}
	
	protected void LoadTimeLabel_PreRender(object sender, EventArgs e)
	{
		timer.Stop();
		LoadTimeLabel.Text = "Page loaded in " + timer.GetTime() + "s";
	}
</script>

<asp:Label ID="LoadTimeLabel" runat="server" OnPreRender="LoadTimeLabel_PreRender" OnInit="LoadTimeLabel_Init"></asp:Label>