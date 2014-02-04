﻿/* 
 * PROJECT: NyARToolkitCS
 * --------------------------------------------------------------------------------
 * This work is based on the original ARToolKit developed by
 *   Hirokazu Kato
 *   Mark Billinghurst
 *   HITLab, University of Washington, Seattle
 * http://www.hitl.washington.edu/artoolkit/
 *
 * The NyARToolkitCS is C# edition ARToolKit class library.
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
using System;
using System.Collections.Generic;
using System.Text;

namespace jp.nyatla.nyartoolkit.cs.core
{
    public class NyARI64Linear
    {
        public long rise;//y軸の増加量
        public long run;//x軸の増加量
        public long intercept;//切片
        public void copyFrom(NyARI64Linear i_source)
        {
            this.rise = i_source.rise;
            this.run = i_source.run;
            this.intercept = i_source.intercept;
            return;
        }
        public static NyARI64Linear[] createArray(int i_number)
        {
            NyARI64Linear[] ret = new NyARI64Linear[i_number];
            for (int i = 0; i < i_number; i++)
            {
                ret[i] = new NyARI64Linear();
            }
            return ret;
        }
    }
}
