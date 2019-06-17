
<style>
.after-icon {
  margin-left: 20px;
}
.stat-item {
  margin: 5px 10px;
}
</style>

<template>
  <div>
    <Row>
      <Col span="6" style="text-align:left;">
        <Card style="margin:10px 20px;">
          <p slot="title">系统配置</p>
          <p class="stat-item">Server Name:ketrel</p>
          <p class="stat-item">Port：</p>
          <p class="stat-item">BackLog：</p>
          <p class="stat-item">No Delay：</p>
          <p class="stat-item">Request Timeout：</p>
          <p class="stat-item">Response Timeout：</p>
          <p class="stat-item">BackLog：</p>
          <p class="stat-item">No Delay：</p>
          <p class="stat-item">MaxConnection：</p>
          <p class="stat-item">ListenType：</p>
          <p class="stat-item">Ssl Enabled：</p>
          <p class="stat-item">Ssl Version：</p>
          <p class="stat-item">Hand Shake Timeout：</p>
          <p class="stat-item">Static File Enable ：</p>
          <p class="stat-item">Virture Path Root：</p>
        </Card>
      </Col>

      <Col span="6" style="text-align:left;">
        <Card style="margin:10px 20px;">
          <p slot="title">App配置</p>
          <p class="stat-item">App Version：</p>
          <p class="stat-item">Dispatcher：</p>
          <p class="stat-item">MiddleWare：</p>
          <p class="stat-item">Service Group Count：</p>
          <p class="stat-item">Total Service Count：</p>
          <p class="stat-item">Test File Genarated：</p>
          <p class="stat-item">Description File Genarated：</p>
        </Card>
      </Col>

      <Col span="6" style="text-align:left;">
        <Card style="margin:10px 20px;">
          <p slot="title">系统信息</p>
          <p class="stat-item">操作系统：</p>
          <p class="stat-item">cpu架构：</p>
          <p class="stat-item">运行时长：</p>
          <p class="stat-item">Cpu使用：</p>
          <p class="stat-item">内存使用：</p>
        </Card>
      </Col>
      
    </Row>

    <Row>
      <div>
        <Card>
          <p slot="title">已部署的Restful 服务</p>
          <Table stripe :columns="columns1" :data="data1"></Table>
        </Card>
      </div>
    </Row>
  </div>
</template>
<script>
export default {
  data() {
    return {
      summary: {
        cpuUsage: "0",
        memoryUsage: 1000,
        memoryTotal: 2000,
        runningTime: "7-15-31-10",
        port: 10336,
        systemVersion: "windows 10",
        failedCount: 0,
        applicationVersion: "",
        cpuArcheteture: "",
        totalVisitCount: 0,
        computorName: ""
      },
      columns1: [
        {
          title: "地址",
          key: "name"
        },
        {
          title: "说明",
          key: "age"
        },
        {
          title: "方法",
          key: "address"
        },
        {
          title: "总访问量",
          key: "age"
        },
        {
          title: "失败次数",
          key: "address"
        }
      ]
    };
  }
};
</script>
