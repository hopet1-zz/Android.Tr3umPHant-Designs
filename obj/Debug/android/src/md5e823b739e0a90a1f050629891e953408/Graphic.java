package md5e823b739e0a90a1f050629891e953408;


public class Graphic
	extends android.app.TabActivity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Tr3umPHantDesigns.Graphic, Tr3umPHant-Designs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Graphic.class, __md_methods);
	}


	public Graphic () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Graphic.class)
			mono.android.TypeManager.Activate ("Tr3umPHantDesigns.Graphic, Tr3umPHant-Designs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
