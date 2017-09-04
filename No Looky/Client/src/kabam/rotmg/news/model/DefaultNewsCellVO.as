﻿package kabam.rotmg.news.model {
public class DefaultNewsCellVO extends NewsCellVO {

    public function DefaultNewsCellVO(_arg_1:int) {
        imageURL = "data:image/jpeg;base64,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";
        linkDetail = "https://discord.com/";
        headline = (((_arg_1 == 0)) ? "Discord" : "Changelog");
        startDate = (new Date().getTime() - 0x3B9ACA00);
        endDate = (new Date().getTime() + 0x3B9ACA00);
        networks = ["kabam.com", "kongregate", "steam", "rotmg"];
        linkType = NewsCellLinkType.OPENS_LINK;
        priority = 999999;
        slot = _arg_1;
    }

}
}//package kabam.rotmg.news.model