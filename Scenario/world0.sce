// 公主城堡
world 0 
// 公主殿下的生日party
scene 0
// 主要角色登场
cut 0
皇家妹抖 公主殿下驾到！
// 公主殿下出现
create 1 10 9
// 公主殿下下楼
downstairs
公主殿下 感谢大家来到我的舞会。
公主殿下 一直以来，有了大家的帮助，王国才能如此和平。
公主殿下 希望能和大家一起创造更好的未来！
公主殿下 那么，请尽情享受舞会吧！
plot 001

cut 1
create 1 10 4.5

npccut 0
皇家妹抖 今天是公主殿下的生日，但是公主殿下今年几岁呢？

npccut 1
皇家妹抖 我们皇家妹抖都是经过严格挑选和训练培养出来的精英妹抖。
牛奶酱 即使这样也不至于完全一样吧…

npccut 2
牛奶酱 你的样子有点奇怪呢！
皇家妹抖？ 怎么？
牛奶酱 你的耳朵很短，相对的尾巴却很长。
皇家妹抖？ 我来自遥远的卡特王国，所以样子和本国的居民会有点区别啦。
牛奶酱 原来是外国人啊。但是卡特王国…没听说过呢…
牛奶酱 不过还有一件事情非常可疑…
皇家妹抖？ 什…什么？
牛奶酱 为什么你会有头像呢？按道理讲只有我这样的主要角色才会有头像的！
皇家妹抖？ 。
EndingFastest

npccut 3
牛奶酱 公主殿下生日快乐！
公主殿下 谢谢。
牛奶酱 公主殿下能邀请我参加生日聚会真是太好了。
公主殿下 牛奶酱今天也很漂亮呢。其实，没有牛奶酱的魔法保护，王国也不会这么和平呢。
牛奶酱 是吗？即使我是来历不明的魔女也没关系吗？
公主殿下 哪里，牛奶酱对于我们而言，是女神一样的存在呢。
牛奶酱 哈哈哈，或许我真的是从天而降的女神呢！
公主殿下 不记得过去也没有关系，我们兔耳族自身也还有很多未解之谜呢。重要的还是要活好现在。
牛奶酱 公主殿下说的是呢。来干杯……啊
公主殿下 怎么样，没事吧？
牛奶酱 这突然的震动是怎么回事啊？害人家把裙子都弄脏了！
公主殿下 不好，是敌袭！
牛奶酱 是谁这么讨人厌啊！看老娘出去收拾她们一顿！
公主殿下 那个…出口在这边…
牛奶酱 我要先去洗个澡换个衣服！
公主殿下 。
// 牛奶酱移动
charamove 19
plot 002

cut 2
gotoscene 0Castle1Outside

cut 3
皇家妹抖？ 为什么要攻击人家啦喵
牛奶酱 为什么？这还用问吗？你的语癖已经暴露了你的身份！
皇家妹抖？ 语癖是什么喵？
牛奶酱 语癖就是每一个种族所特有的，会在句尾不自觉地加上的声音！而你的语癖，明显不是我们兔耳族的！
皇家妹抖？ 兔耳族的语癖是什么喵，为什么你没有喵？
牛奶酱 我当然有了，就是
皇家妹抖？ 那是什么啊喵，为什么人家完全听不到的喵。
牛奶酱 那是因为…你的耳朵太小了！
皇家妹抖？ （受到了强烈的鄙视）
牛奶酱 你那尖尖的耳朵，和你那奇怪的讲话方式，答案只有一个，你就是…
皇家妹抖？ （什么，要暴露了吗？）
牛奶酱 精灵族！待在森林里，擅长玩弓，喜欢穿树叶的种族！
皇家妹抖？ 人家才不是精灵族呢喵，你真笨啊喵，人家明明是在宇宙中旅行的高贵的猫耳族喵，这次来月球，就是为了掳走你们的公主喵，所以才假装皇家妹抖混进了王宫喵。这么完美的计划，你们愚蠢的兔耳族怎么可能理解喵！
牛奶酱 这个游戏的反派，智商这么低的吗？
皇家妹抖 原来你不是皇家妹抖的一员，那么我们要没收你的制服。
皇家妹抖？ 不要！不要拿走人家的制服喵！人家好不容易才能穿上这种适合贫乳的妹抖服，不能拿走喵！只要不拿走，叫人家做什么都行！
牛奶酱 既然这样，那么…

// 王宫外
scene 1
npccut 0
// 教学
皇家妹抖 没想到战斗这么快就开始了，你做好准备了吗？
皇家妹抖 使用←→来移动，Ⓨ攻击，Ⓑ跳跃，在空中按下Ⓑ还可以进行二段跳。
皇家妹抖 说来，二段跳很不科学呢，不过你是魔女对不对啊？

cut 0
// 草莓汁登场
草莓汁 宇宙最强的猫耳机器人，草莓汁大人登场了！ // 果汁四天王：草莓汁、西瓜汁、桔子汁、葡萄汁，只可惜另几个角色没机会登场了（
牛奶酱 你哪位？
草莓汁 我不是刚刚说过了吗，注意听人家的登场台词是最基本的礼貌！
牛奶酱 好的，我知道了，回头见。
草莓汁 回来！你还不了解你现在的处境吗？你即将被我的炮火变成红烧兔子！
牛奶酱 你还是先看下自己的处境吧，都卡在桥里了。
草莓汁 什么？啊，怎么会这样！我该怎么办？
牛奶酱 卡住的是你的机器人又不是你，你跳下来不就好了。
草莓汁 不可以！这可是可乐炭大人送我的最新发明，搭载了白痴也能立刻学会的体感操作系统，我将它命名为Mega Destroyer Zero Zero！
牛奶酱 MDZZ。
草莓汁 其实，我这是为了阻止你的去路而事先设计好的套路，想不到吧？
牛奶酱 想不到。
草莓汁 总之，我要立刻干掉你就是了！
boss 0

cut 1
皇家妹抖 大事不好了！
牛奶酱 怎么了？
皇家妹抖 公主殿下…公主殿下被抓走了！
牛奶酱 什么！
皇家妹抖 有个长得跟我们很像的家伙混进了皇家妹抖群中，然后趁刚才混乱的时候，带走了公主殿下！
牛奶酱 是那个家伙吗…
牛奶酱 这件事情就包在我身上！我这就去把公主殿下救出来！
皇家妹抖 拜托了！
牛奶酱 公主殿下的声音…是从前面传来的…
