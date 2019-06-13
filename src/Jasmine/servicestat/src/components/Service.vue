<template>
  <div class="service-display">
    <Card>
      <!-- title -->
      <Row>
       <Card style="text-align:left;">
         <p slot="title">
          <Icon type="ios-cloud" size="45"/>系统信息  <Badage></Badage>  <span> <Button>启动</Button><Button>停止</Button></span>
          </p>
          <p>
          </p>
       </Card>
      </Row>
      <!-- description-->
      <Row>
        <p>{{this.$store.state.currentService.description}}</p>
      </Row>
      <!-- table -->
      <Row>
        <Table :columns='columns1'></Table>
      </Row>
      <!-- stat-->
      <Row></Row>
    </Card>

    <div id="stat">
      <a @click="refresh">click me</a>
    </div>
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
          title: "类型",
          key: "age"
        },
        {
          title: "说明",
          key: "address"
        },
        {
          title: "示例值",
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
