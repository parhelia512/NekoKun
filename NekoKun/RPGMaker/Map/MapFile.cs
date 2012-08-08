﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NekoKun.RPGMaker
{
    public class MapFile : AbstractFile
    {
        public string Title;
        private bool infoLoaded = false;
        public System.Drawing.Size Size;
        public TilesetInfo tileset;
        public RubyBindings.RGSSTable data;

        public MapFile(string filename)
            : base(filename)
        {
            
        }

        protected override void Save()
        {
            throw new NotImplementedException();
        }

        public override AbstractEditor CreateEditor()
        {
            if (!infoLoaded)
                LoadInfo();
            return new MapEditor(this);
        }

        private void LoadInfo()
        {
            infoLoaded = true;
            tileset = new TilesetInfo();

            RubyBindings.RubyObject raw = RubyBindings.RubyMarshal.Load(new System.IO.FileStream(this.filename, System.IO.FileMode.Open, System.IO.FileAccess.Read)) as RubyBindings.RubyObject;
            this.data = raw["@data"] as RubyBindings.RGSSTable;
            this.Size = new System.Drawing.Size((int)raw["@width"], (int)raw["@height"]);
        }
        /*
          @tileset_id = 1
          @width = width
          @height = height
          @autoplay_bgm = false
          @bgm = RPG::AudioFile.new
          @autoplay_bgs = false
          @bgs = RPG::AudioFile.new("", 80)
          @encounter_list = []
          @encounter_step = 30
          @data = Table.new(width, height, 3)
          @events = {}  
        */
    }
}
