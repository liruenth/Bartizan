using Patcher;
using TowerFall;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Mod
{
	[Patch]
	public class MyLevel : Level
  {
		public MyLevel (Session session, XmlElement xml) : base(session, xml)
		{
		}

		public override void Update ()
		{
			base.Update();
			List<Entity> teamRevivers = base[GameTags.Dummy]; // Using Dummy tag for MyTeamReviver
			for (int i = 0; i < teamRevivers.Count; i++) {
				MyTeamReviver teamReviver = (MyTeamReviver)(teamRevivers[i]);
				if (teamReviver.Active) {
					teamReviver.ReviveUpdate ();
				}
			}
		}

		public override void HandlePausing ()
		{
			if (((MyMatchVariants)this.Session.MatchSettings.Variants).GottaBustGhosts) {
				// Let game be paused while ghost busting
				bool wasEnding = this.Ending;
				this.Ending = false;
				base.HandlePausing();
				this.Ending = wasEnding;
			} else {
				base.HandlePausing();
			}
		}
  }
}
