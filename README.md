# Vertex_HUD

相关教程和测试结果

https://www.zybuluo.com/daaoling/note/589784


# 相关说明

出于顶点位置的限制，那么最多只有后面的3个关键帧

默认最多的pop时间不能超过3秒 3秒之后会被回收， 或者你自己设置

    newPopEntry.totalRuningTime = 3.0f;