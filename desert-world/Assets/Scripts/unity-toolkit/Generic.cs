namespace UTK {

    public static class Generic : object {

        // Source: https://forum.unity.com/threads/re-map-a-number-from-one-range-to-another.119437/
        public static float MapIntoRange (float value, float from1, float to1, float from2, float to2) {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        // does this work or do we need to use floats as intermediate to calculate correctly?
        public static int MapIntoRange (int value, int from1, int to1, int from2, int to2) {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

    }

}
