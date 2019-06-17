<template>
  <div class="service-display">
    <!-- title -->
    <Row>
      <p>
        <Badage>当前出状态</Badage>
        <span>
          <Button>启动</Button>
          <Button>停止</Button>
        </span>
      </p>
      <p>description</p>
    </Row>
    <!-- input ouput description  -->
    <Row>
      <p>{{this.$store.state.currentService.description}}</p>
    </Row>
    <!-- filter s -->
    <Row>
      <Col>
        <Table :columns="columns1"></Table>
      </Col>
      <Col>
        <Table></Table>
      </Col>
    </Row>
    <!-- filters -->
    <Row></Row>
    <!-- log -->
    <Row></Row>

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
          title: "位置",
          key: "age"
        },
        {
          title: "类型",
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
