<style>
.title {
  text-align: left;
  border-bottom: solid 2px grey;
  font-size: 20px;
  margin-bottom: 20px;
  padding: 3px;
}
.text-align-left {
  text-align: left;
  font-size: 14px;
}
.margin-top-20 {
  margin-top: 20px;
}
.article {
  padding: 20px;
  text-align: left;

  border-radius: 10px;
  background-color: rgb(235, 235, 235);
}
.margin-left-20 {
  margin-left: 30px;
}
</style>

<template>
  <div class="service-display">
    <!-- title -->
    <Divider orientation="left">服务概要</Divider>
    <Row class="text-align-left">
      <Card>
        <span class="margin-left-20">{{this.$store.state.currentService.name}}</span>
        <span class="margin-left-20">地址 ：{{this.$store.state.currentService.path}}</span>
        <span class="margin-left-20">启动时间： {{this.$store.state.currentService.total}}</span>
        <span class="margin-left-20">总访问量： {{this.$store.state.currentService.total}}</span>
        <span class="margin-left-20">失败次数：{{this.$store.state.currentService.failed}}</span>
        <span class="margin-left-20">
          当前出状态
          <Badge status="success"/>
        </span>
        <span class="margin-left-20">
          <Button>启动</Button>
          <Button>停止</Button>
        </span>
      </Card>
    </Row>
    <!-- input ouput description  -->
    <Divider orientation="left">说明</Divider>
    <Row class="margin-top-20">
      <Card>
        <p>{{this.$store.state.currentService.description}}</p>
      </Card>
    </Row>
    <!-- filter s -->
    <Divider orientation="left">处理流</Divider>
    <Row class="margin-top-20">
      <Steps :current="8">
        <Step title="filter1" content="这里是该步骤的描述信息"></Step>
        <Step title="filter2" content="这里是该步骤的描述信息"></Step>
        <Step title="filter3" content="这里是该步骤的描述信息"></Step>
        <Step title="filter4" content="这里是该步骤的描述信息"></Step>
        <Step title="filter1" content="这里是该步骤的描述信息"></Step>
      </Steps>
    </Row>
    <!-- filter s -->
    <Divider orientation="left">错误流</Divider>
    <Row class="margin-top-20">
      <Steps :current="8">
        <Step title="filter1" content="这里是该步骤的描述信息"></Step>
        <Step title="filter2" content="这里是该步骤的描述信息"></Step>
        <Step title="filter3" content="这里是该步骤的描述信息"></Step>
        <Step title="filter4" content="这里是该步骤的描述信息"></Step>
      </Steps>
    </Row>
    <!-- filters -->
    <Divider orientation="left">参数和返回</Divider>
    <Row>
      <Col span="12">
        <Card>
          <Table :columns="columns1"/>
        </Card>
      </Col>
      <Col span="12"></Col>
    </Row>
    <!-- mertric -->
    <Divider orientation="left">Debug</Divider>
    <Row>
      <Table :columns="columns2"/>
    </Row>

    <!-- mertric -->
    <Divider orientation="left">统计分析</Divider>
    <Row></Row>
  </div>
</template>

<script>
export default {
  data() {
    return {
      columns1: [
        {
          title: "名称",
          key: "name"
        },

        {
          title: "位置",
          key: "age"
        },
        {
          title: "类型",
          key: "age"
        },
        {
          title: "必须",
          key: "age"
        },
        {
          title: "示例值",
          key: "address"
        },
        {
          title: "说明",
          key: "age"
        }
      ],
      columns2: [
        {
          
          title: "Method",
          key: "name"
        },
        {
           
          title: "TraceId",
          key: "name"
        },

        {
           
          title: "Time",
          key: "age"
        },
        {
           
          title: "Remote",
          key: "age"
        },
        {
          
          title: "Query",
          key: "age"
        },
        {
           width: 300,
          title: "Body",
          key: "address"
        },
        {
           
          title: "Cookie",
          key: "age"
        },
        {
           
          title: "PathVarible",
          key: "age"
        },
        {
          
          title: "Headers",
          key: "age"
        },
        {
           width: 300,
          title: "Error",
          key: "age"
        },
        { 
          
          title: "Elapse",
          key: "age"
        }
      ]
    };
  },

  methods: {
    refresh() {
      var result = this.$store.state.visitor.getResponse("/getall");

      console.log(result);

      this.$store.commit("refreshService");
    },
    shutDown() {
      this.$store.commit("shutdownService");
    },
    resume() {
      this.$store.commit("resumeService");
    },
    remove() {
      this.$store.commit("removeService");
    }
  }
};
</script>
