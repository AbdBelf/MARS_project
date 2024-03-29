/* 
 * PROJECT: NyARToolkit(Extension)
 * --------------------------------------------------------------------------------
 * The NyARToolkit is Java edition ARToolKit class library.
 * Copyright (C)2008-2009 Ryo Iizuka
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * For further information please contact.
 *	http://nyatla.jp/nyatoolkit/
 *	<airmail(at)ebony.plala.or.jp> or <nyatla(at)nyatla.jp>
 * 
 */
using jp.nyatla.nyartoolkit.cs.core;
namespace jp.nyatla.nyartoolkit.cs.core
{
    public class NyARLCGsRandomizer
    {
        protected long _rand_val;
        protected int _seed;
        public NyARLCGsRandomizer(int i_seed)
        {
            this._seed = i_seed;
            this._rand_val = i_seed;
        }
        public void setSeed(int i_seed)
        {
            this._rand_val = i_seed;
        }
        public virtual int rand()
        {
            this._rand_val = (this._rand_val * 214013L + 2531011L);
            return (int)((this._rand_val >> 16) & RAND_MAX);

        }
        public const int RAND_MAX = 0x7fff;
    }
}
