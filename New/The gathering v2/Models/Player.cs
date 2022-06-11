﻿namespace The_gathering_v2.Models
{
    public class Player
    {
        public int Life { get; set; }
        public Hand? Hand { get; set; }
        public DiscardPile? DiscardPile { get; set; }
        public Deck? Deck { get; set; }
        public Board? Board { get; set; }
    }
}