
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
      <Col span="8" style="text-align:left;">
        <Card style="margin:10px 20px;">
          <p slot="title">
            <Icon type="ios-cloud" size="45"/>系统信息
          </p>
          <p class="stat-item">操作系统：{{summary.systemVersion}}</p>
          <p class="stat-item">cpu架构：{{summary.cpuArchetetcture}}</p>
          <p class="stat-item">运行时长：{{summary.runningTime}}</p>
          <p class="stat-item">Cpu使用：{{summary.memoryUsage}}</p>
          <p class="stat-item">内存使用：{{summary.memoryUsage}}</p>
        </Card>
      </Col>

      <Col span="8" style="text-align:left;">
        <Card style="margin:10px 20px;">
          <p slot="title">系统信息</p>
          <p class="stat-item">操作系统：{{summary.systemVersion}}</p>
          <p class="stat-item">cpu架构：{{summary.cpuArchetetcture}}</p>
          <p class="stat-item">运行时长：{{summary.runningTime}}</p>
          <p class="stat-item">Cpu使用：{{summary.memoryUsage}}</p>
          <p class="stat-item">内存使用：{{summary.memoryUsage}}</p>
        </Card>
      </Col>

      <Col span="8" style="text-align:left;">
        <Card style="margin:10px 20px;">
          <p slot="title">系统信息</p>
          <p class="stat-item">操作系统：{{summary.systemVersion}}</p>
          <p class="stat-item">cpu架构：{{summary.cpuArchetetcture}}</p>
          <p class="stat-item">运行时长：{{summary.runningTime}}</p>
          <p class="stat-item">Cpu使用：{{summary.memoryUsage}}</p>
          <p class="stat-item">内存使用：{{summary.memoryUsage}}</p>
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
