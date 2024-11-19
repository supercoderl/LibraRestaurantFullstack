<p align="center">
	<img alt="logo" src="https://github.com/huanghanzhilian/huanghanzhilian/raw/main/projects/c-shopping-rn.svg" width="300">
</p>
<h1 align="center" style="margin: 30px 0 30px; font-weight: bold;">C-Shopping-RN APP</h1>
<h4 align="center">这个是一个React Native(Expo) 开发的完整App应用，是一个精美的电商购物应用</h4>

## README.md

- en [English](README.md)
- zh_CN [简体中文](README.zh_CN.md)

## 使用技术

- React Native
- Redux Toolkit
- RTK Query
- Expo Router
- NativeWind

## 前言

`c-shopping-rn`是一个精美的电商App，界面优雅，功能丰富，小巧迅速，包含一个电商平台MVP完整功能，具备良好的审美风格与编码设计。

项目数据接口来自于[c-shopping](https://github.com/huanghanzhilian/c-shopping) Next.js 开源项目

项目传送门: [https://github.com/huanghanzhilian/c-shopping](https://github.com/huanghanzhilian/c-shopping)

希望来的人，有所收获。故事不结束，青春不散场。

## 项目介绍

**背景：**

- 一直以来前端UI框架被固定形式占据（受限于传统的UI框架），导致视觉疲劳，在开发一些高度自定义的项目时，往往力不从心；
- 多设备适配的web优秀项目很少，学习和维护成本较高；
- 当项目复杂后，在组件需要调用多个 api 时会变得复杂起来，比如需要管理多个 loading 和 error 状态，这会导致产生非常多的 state 声明，还有请求取消、请求竞态等可能存在的问题也容易被忽略；
- 随着项目复杂，样式的开发与维护变得庞大且臃肿；

**意图：**

改进背景中提到的问题。

**目的：**

打造一个完整的，适合web端的良好生态。

## 项目演示

| Module | loading                                                                                        | skeleton                                                                                       | success                                                                                        | error or empty                                                                                 |
| ------ | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| feed   | ![](https://www.cheerspublishing.com/uploads/article/e1f91a01-bf5e-46f4-8cbd-7f485e5039ba.png) | ![](https://www.cheerspublishing.com/uploads/article/d09fc3af-1bf7-49fa-8957-227a3add172a.png) | ![](https://www.cheerspublishing.com/uploads/article/efee1d79-9c95-4be5-9a6a-ec5160b7e6ae.png) | ![](https://www.cheerspublishing.com/uploads/article/c3d29b64-c6be-4716-8b4d-30becfbe4246.png) |

| Module   | loading                                                                                        | success                                                                                        | empty                                                                                          | error                                                                                          |
| -------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| Category | ![](https://www.cheerspublishing.com/uploads/article/5d6451e5-1a71-472d-851d-08049a37df9e.png) | ![](https://www.cheerspublishing.com/uploads/article/302d74e3-b4ef-4d90-87b7-e57d9217caa2.png) | ![](https://www.cheerspublishing.com/uploads/article/b164744f-a5cd-4f9c-a3a5-b0b33b90b82d.png) | ![](https://www.cheerspublishing.com/uploads/article/06c64210-4f8e-4b43-9f1d-9b5c1b2748a4.png) |

| Module                | Loading and skeleton                                                                           | Level 1                                                                                        | Level 2                                                                                        | Level 3                                                                                        | Filter                                                                                         | Sort                                                                                           |
| --------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| Classification subset | ![](https://www.cheerspublishing.com/uploads/article/be7d64db-496d-442f-a1e6-ae7d42e9b628.png) | ![](https://www.cheerspublishing.com/uploads/article/3be1701c-ed03-4394-984a-9ff9b67dd111.png) | ![](https://www.cheerspublishing.com/uploads/article/44c1ac35-c6a6-4736-8c49-2a030afe14c8.png) | ![](https://www.cheerspublishing.com/uploads/article/f54cdf46-72c0-42bc-ade2-9c3df96cd121.png) | ![](https://www.cheerspublishing.com/uploads/article/e4733e24-fa72-4ee0-9c33-c0dbc8072594.png) | ![](https://www.cheerspublishing.com/uploads/article/0229b9d8-4785-48fb-9028-f7a00cdccfca.png) |

| Module          | Loading and skeleton                                                                           | Normal screen 1                                                                                | Normal screen 1                                                                                | Error                                                                                          |
| --------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| product details | ![](https://www.cheerspublishing.com/uploads/article/08946a24-197c-4fc0-9a44-0950621f90b7.png) | ![](https://www.cheerspublishing.com/uploads/article/43a51e87-8775-41b8-a6a4-519e256015e4.png) | ![](https://www.cheerspublishing.com/uploads/article/fc28819a-456c-4a50-822b-1fa395198b66.png) | ![](https://www.cheerspublishing.com/uploads/article/a066c4c3-ebf1-460e-91c1-ad5431f04cb9.png) |

| Module         | Comment section                                                                                | Click to verify login                                                                          | Fill in the comments                                                                           | Submit comment error                                                                           | Comment submitted successfully                                                                 |
| -------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| product review | ![](https://www.cheerspublishing.com/uploads/article/031b85f9-db43-49b6-9f1b-13209b9bb7ef.png) | ![](https://www.cheerspublishing.com/uploads/article/b07813d7-bf0d-4fe5-80d7-625dd561e4a4.png) | ![](https://www.cheerspublishing.com/uploads/article/d8048925-dca5-4330-a753-69e3af19bcd3.png) | ![](https://www.cheerspublishing.com/uploads/article/e935116f-1ab4-4877-90f0-dac66031f0b9.png) | ![](https://www.cheerspublishing.com/uploads/article/26faa410-3b6a-4882-9373-9992843f6d7e.png) |

| Module | Not logged in                                                                                  | Data is empty                                                                                  | Screen 1                                                                                       | Screen 2                                                                                       | add to the cart                                                                                |
| ------ | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| Cart   | ![](https://www.cheerspublishing.com/uploads/article/64a07f64-96fd-4129-9a75-a27cc681e839.png) | ![](https://www.cheerspublishing.com/uploads/article/4a00d6af-a0f0-42e8-8e3b-ed44464cb5da.png) | ![](https://www.cheerspublishing.com/uploads/article/cee4592d-16e1-4836-8d91-6c79608c1eec.png) | ![](https://www.cheerspublishing.com/uploads/article/ed79f4cf-db93-4c12-beeb-66503ef31810.png) | ![](https://www.cheerspublishing.com/uploads/article/20f87dfc-7b5f-4109-8098-0e26db8c8b57.png) |

| Module  | Shopping cart to pay                                                                           | Confirm payment                                                                                | payment successful                                                                             |
| ------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| Payment | ![](https://www.cheerspublishing.com/uploads/article/7f1ce096-7624-4102-9e64-ec562693ee42.png) | ![](https://www.cheerspublishing.com/uploads/article/33b2435e-5146-425c-a514-c8ab725909f6.png) | ![](https://www.cheerspublishing.com/uploads/article/bd619f57-6f04-44e8-94ac-ccb3a752dc6a.png) |

| Module  | Not logged in                                                                                  | After logging in                                                                               | After logging out                                                                              |
| ------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| Profile | ![](https://www.cheerspublishing.com/uploads/article/619b8402-79b6-442a-8fed-62763b4163e6.png) | ![](https://www.cheerspublishing.com/uploads/article/b30c1355-3f4f-47de-8087-7a7e7e55e451.png) | ![](https://www.cheerspublishing.com/uploads/article/a37a6421-784d-441f-aa8c-c3212654bc4e.png) |

| Module                | A page that requires login                                                                     | Before the button logic, you need to verify whether you are logged in and After entering the page, you need to verify whether you are logged in and redirect | login display error                                                                            | login loading                                                                                  | login handle response                                                                          | register                                                                                       |
| --------------------- | ---------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------ | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| Authentication design | ![](https://www.cheerspublishing.com/uploads/article/f804fc4b-e1a8-4837-8685-2d7c38ee80ed.png) | ![](https://www.cheerspublishing.com/uploads/article/48581171-9bcd-4154-bf36-0a87a44fc1e4.png)                                                               | ![](https://www.cheerspublishing.com/uploads/article/4dd13db6-7360-4faf-9be5-7fd3fa569f50.png) | ![](https://www.cheerspublishing.com/uploads/article/b07813d7-bf0d-4fe5-80d7-625dd561e4a4.png) | ![](https://www.cheerspublishing.com/uploads/article/fbbcf9d2-d75b-418d-a942-b489b9fc13fb.png) | ![](https://www.cheerspublishing.com/uploads/article/9ac75cc6-dd60-42f4-b8a7-ebc655f13e44.png) |

| Module      | Loading or skeleton                                                                            | Normal screen                                                                                  | Empty screen                                                                                   | Error                                                                                          |
| ----------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| Orders List | ![](https://www.cheerspublishing.com/uploads/article/df2e2fa4-cc44-4200-8987-c9bbbff0d849.png) | ![](https://www.cheerspublishing.com/uploads/article/85bc15cc-fb22-4164-ad16-d5fee2e090fa.png) | ![](https://www.cheerspublishing.com/uploads/article/3b3d72a9-a30c-4fe0-9d84-a6a3443e9818.png) | ![](https://www.cheerspublishing.com/uploads/article/08c8202a-323d-476d-8255-b982ee5ba775.png) |

| Module       | Loading or skeleton                                                                            | Normal screen                                                                                  | Empty screen                                                                                   | Error                                                                                          |
| ------------ | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| Reviews List | ![](https://www.cheerspublishing.com/uploads/article/421bca65-41cb-448f-a20b-52ce194fb96e.png) | ![](https://www.cheerspublishing.com/uploads/article/9e6945a8-fd31-4e7a-a959-8f95745d51cc.png) | ![](https://www.cheerspublishing.com/uploads/article/1fb3fc3b-61c4-473c-ac1c-8ad7c564c53c.png) | ![](https://www.cheerspublishing.com/uploads/article/13aa858f-1207-43f8-9cd6-270e7e3e44a4.png) |

| Module              | Loading or skeleton                                                                            | Normal screen                                                                                  | Edit and save                                                                                  |
| ------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| PersonalInfo Screen | ![](https://www.cheerspublishing.com/uploads/article/c2709941-86e0-42e7-881e-4d44ad2bc009.png) | ![](https://www.cheerspublishing.com/uploads/article/ba048862-c706-4f6e-94af-18f8981e6b0c.png) | ![](https://www.cheerspublishing.com/uploads/article/6d892150-d74c-41e4-b704-64868eddbe29.png) |

| Module           | Loading or skeleton                                                                            | Normal screen                                                                                  | Empty screen                                                                                   |
| ---------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| Addresses Screen | ![](https://www.cheerspublishing.com/uploads/article/cf6b9e9a-2cbd-4ac1-96e8-94e74e58564c.png) | ![](https://www.cheerspublishing.com/uploads/article/affe57f6-fdc3-40cf-bca5-89201081d564.png) | ![](https://www.cheerspublishing.com/uploads/article/ef73752f-b5f8-4ef8-8b2c-9b179c803946.png) |

| Module        | Loading or skeleton                                                                            | Normal screen                                                                                  | Empty screen                                                                                   |
| ------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| Serach Screen | ![](https://www.cheerspublishing.com/uploads/article/f858a528-e810-4aaa-8b38-15a093ca6fc0.png) | ![](https://www.cheerspublishing.com/uploads/article/ce3a5199-bb02-4276-8f78-422ab41d68fd.png) | ![](https://www.cheerspublishing.com/uploads/article/cb818668-b4af-488a-9991-cc5923c2e915.png) |

## 运行使用

本地运行 c-shopping-rn React Native 应用程序，请按照以下步骤操作：

1. 克隆存储库：

   ```
   git clone https://github.com/huanghanzhilian/c-shopping-rn.git
   ```

2. 导航到项目目录：

   ```
   cd c-shopping-rn
   ```

3. 安装依赖项：

   ```
   npm install
   ```

4. 查看`.env.example`内容，创建新的`.env`的文件，在项目根目录，定义所需的环境变量。
   ```
   EXPO_PUBLIC_BASE_URL=https://c-shopping-three.vercel.app
   ```
5. 启动：

   ```
   npm start
   ```

6. 连接移动设备或模拟器以运行该应用程序。

## 关于我

I am a technology explorer, a eager learner, and a problem solver.
<br/>
我是一个技术的探索者，一个渴望学习的人，一个解决问题的人。

- Email: [h1319639755@gmail.com](mailto:h1319639755@gmail.com)
- Twitter: [继小鹏](https://twitter.com/Huanghanzhilian)
- Github: [Github](https://github.com/huanghanzhilian)
- Blog: [继小鹏](https://blog.huanghanlian.com/)
- 我的中文渠道:
  - 微博：[继小鹏1](https://weibo.com/u/5653497957)
  - 微信公众号：「继小鹏的博客」
  - 掘金：[继小鹏](https://juejin.cn/user/2119514150414686)
  - 知乎：[继小鹏](https://www.zhihu.com/people/huang-han-zhi-lian)
  - 即刻：[继小鹏](https://web.okjike.com/u/BA3424F7-4D6E-4A2C-BA7E-138B616EED05)
  - bilibili：[继小鹏](https://space.bilibili.com/191128853)

### 公众号｜我的微信

关注公众号获得更多资讯，有任何意见或建议都欢迎提issue，或者到公众号留言；也欢迎加我微信交流。

| 我的公众号                                                                                                                            | 我的微信                                                                                                                         |
| ------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------- |
| <img alt="继小鹏公众号" src="https://www.cheerspublishing.com/uploads/article/4632461d-0d43-4378-bcf7-bb32bf0de950.jpeg" width="150"> | <img alt="我的微信" src="https://www.cheerspublishing.com/uploads/article/6f041fb6-2141-49d8-b20b-6e989687bf9a.png" width="150"> |

## 贡献

欢迎贡献！ 请提出问题或提交拉取请求。

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

This README provides a brief overview of the project's code structure and how to install and run the React Native app locally. Adjustments can be made to tailor it further to your specific project.
