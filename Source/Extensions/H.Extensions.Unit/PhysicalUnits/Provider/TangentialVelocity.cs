namespace H.Extensions.Unit
{
    public partial struct TangentialVelocity
    {
        public static implicit operator TangentialVelocity(Velocity v)
        {
            return new TangentialVelocity(v.Value);
        }

    }
}
