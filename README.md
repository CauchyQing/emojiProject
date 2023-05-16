# emojiProject
多人2D格斗游戏《emoji大乱斗》
项目文件夹
Asset：
	Animations	//存人物的动作
	Art Assets：	//存放美术资源文件
		GUIArt	//GUI美术素材
		PlayerArt	//人物美术素材
		MapArt	//地图美术素材
		BGM	//BGM素材
	Scenes：		//存放场景.unity文件
		GUI		//存放GUI场景.unity文件
		Playground	//存放游戏地图
	Scripts：		//存放脚本文件
		General	//存放攻击判定、物理碰撞等文件
		Player	//人物控制及人物动画控制文件
	Settings：	//一些设置文件
		Input System	//输入控制文件
		Physics Material	//物理摩擦材质等
	Tilemap		//瓦片地图文件，需要创建瓦片地图在此文件夹下创建
	

命名规范：
1、文件、函数名及组件的名字用英文全称命名，尽量简洁，比如普通攻击：AccumulatedAttack
2、script里面的变量单词第一个小写，之后的单词首字母大写，如isGround
