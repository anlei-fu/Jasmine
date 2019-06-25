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
   
    <!-- mertric -->
    <Divider orientation="left">统计分析</Divider>
    <Row></Row>
  </div>
</template>
<script>
export default {
    
}
</script>
