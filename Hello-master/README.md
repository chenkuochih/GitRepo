# 第一次作业
### 思路 <br />
     添加一行Console.ResetColor();将所有设置的样式清除
### 代码如下 <br />
     static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World! {0} is {1}",args[0],args[1]);
            Console.BackgroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Hello World!");
            System.Console.WriteLine("Hello World!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("Hello World!");
            Console.ResetColor();
            System.Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
### 遇到的问题
     安装Visual Studio很顺利，但是在进行GitHub连接与同步时困难重重。
     连接：一步一步跟着廖雪峰老师的步骤进行，但是由于廖雪峰老师填写地址的方式与我电脑不符合，一直未成功。在求助老师更改地址后解决了。
     同步：一直同步过去的不知道是什么文件，最后通过在群里助教的解答才知道应该把写的项目文件与本地库放在同一个文件夹，这个问题也解决了。
<br />
