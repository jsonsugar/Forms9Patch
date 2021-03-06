using System;

namespace FormsGestures
{
	/// <summary>
	/// FormsGestures Pinch event arguments.
	/// </summary>
	public class PinchEventArgs : BaseGestureEventArgs {
		
        /// <summary>
        /// Distance of most recent sampling of pinch motion
        /// </summary>
		public virtual double Distance { get; protected set; }

        /// <summary>
        /// Scale of most recent sample of pinch motion (relative to first sample)
        /// </summary>
		public virtual double DeltaScale { get; protected set; }

        /// <summary>
        /// Total change in scale of pinch motion
        /// </summary>
		public virtual double TotalScale { get; protected set; }


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="source"></param>
        /// <param name="newListener"></param>
		public PinchEventArgs(PinchEventArgs source=null, Listener newListener=null) : base(source, newListener) {
			if (source != null) {
				Distance = source.Distance;
				DeltaScale = source.DeltaScale;
				TotalScale = source.TotalScale;
			}
		}

        /// <summary>
        /// Calculates total and most recent change in scale
        /// </summary>
        /// <param name="previous"></param>
		protected void CalculateScales(PinchEventArgs previous) {
			if (previous != null && Touches.Length > 1 && previous.Touches.Length > 1) {
				Distance = GetDistance (previous);
			} else if (Touches.Length > 1) {
				Distance = GetDistance ();
			} else if (previous != null)
				Distance = previous.Distance;
			else
				Distance = 0.0;
			if (previous == null) {
				DeltaScale = 1.0;
				TotalScale = 1.0;
				return;
			}
			if (Touches.Length != previous.Touches.Length) {
				DeltaScale = 1.0;
				TotalScale = previous.TotalScale;
				return;
			}
			DeltaScale = Distance / previous.Distance;
			TotalScale = previous.TotalScale * DeltaScale;
		}

		double GetDistance(PinchEventArgs previous) {
			double deltaX = (previous.Touches[1].X + Touches[1].X)/2.0 - (previous.Touches[0].X+Touches[0].X)/2.0;
			double deltaY = (previous.Touches[1].Y + Touches[1].Y)/2.0 - (previous.Touches[0].Y+Touches[0].Y)/2.0;
			var distance = Math.Sqrt (deltaX * deltaX + deltaY * deltaY);
			return distance;
		}

		double GetDistance() {
			double deltaX = Touches[1].X - Touches[0].X;
			double deltaY = Touches[1].Y - Touches[0].Y;
			var distance = Math.Sqrt (deltaX * deltaX + deltaY * deltaY);
			//System.Diagnostics.Debug.WriteLine ("x1=["+ Touches[1].X +"] x0=["+ Touches[0].X +"] y1=["+ Touches[1].Y +"] y0=["+ Touches[0].Y +"]");
			//System.Diagnostics.Debug.WriteLine ("dx="+deltaX+" dy="+deltaY+" Dist="+distance);
			return distance;
		}

		internal PinchEventArgs Diff(PinchEventArgs lastArgs) {
			return new PinchEventArgs {
				Cancelled = Cancelled,
				Handled = Handled,
				ViewPosition = ViewPosition,
				Touches = Touches,
				Listener = Listener,
				Distance = Distance,
				TotalScale = TotalScale,
				DeltaScale = TotalScale / lastArgs.TotalScale
			};
		}

        /// <summary>
        /// Updates properties from the values from another instance
        /// </summary>
        /// <param name="source"></param>
        public void ValueFrom(PinchEventArgs source)
        {
            base.ValueFrom(source);
            Distance = source.Distance;
            DeltaScale = source.DeltaScale;
            TotalScale = source.TotalScale;
        }
	}
}
