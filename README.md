3 ways of executing a large number of actions on top of the .net framework.

* TaskFactory.StartNew() 
( which in the default config and with the default scheduler uses the ThreadPool.QueueWorkItem() )

* ThreadPool.QueueWorkItem()
( using thread pool directly - ok, but syntax is a bit messy )

* Thread.Start()
( not ok, as for each action a new native thread is spawn )