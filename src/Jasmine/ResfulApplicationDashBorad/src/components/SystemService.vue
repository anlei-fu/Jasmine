<template>
  <div>
    <Row ></Row>
    <Table :columns="columns1"/>
  </div>
</template>

<script>
export default {
  data() {
    return {
      columns1: [
        {
          title: "地址",
          key: "name"
        },

        {
          title: "方法",
          key: "age"
        },
        {
          title: "权限",
          key: "age"
        },
        {
          title: "参数",
          key: "age"
        },
        {
          title: "返回",
          key: "address"
        },
        {
          title: "说明",
          key: "age"
        }
      ]
    };
  }
};
</script>
