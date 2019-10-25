module RitualWorkflow   

    //type Undefined = exn

    // 1. identify events ==> order form received (input event), order placed (output/input event)
    // 2. identify workflows by identifing commands and rules (system/policy?) ==> SendOrderForm, PlaceOrder 
    // 3. Identify bounded contexts: Order taking domain, Shipping domain, Billing domain
    // 4. identify Domain Objects (Entities and Value Objects) for input of workflows ==> Order
    // 5. describe workflow: PlaceOrder: 1. validate 2. calculate pricing 3. send ack, send shipping event, send billing event
    // 6. refine Domain Objects (add Domain Objects to model state by type) ==> UnvalidatedOrder, ValidatedOrder, PricedOrder
    // 7. refine workflow: identify workflow sub-steps ==> ValidateOrder, CalculatePricing, SendAck
    // 8. refine workflow: identify dependencies ==> product catalog (ValidateOrder, CalculatePricing), address validator (ValidateOrder)
    // 9. refine workflow: describe output events and side effects ==> events: AckSent, OrderPlaced, BillableOrderPlaced 


    // Very simple MVP: one user - several rituals (CRUD) with several instructions (CRUD), rituals/instructions can be revived to support 
    // prefilled lists like a shopping list (no sorting, no nothing)

    // step 1: events
    // type RitualAdded = RitualCreated of Undefined
    // type InstructionAdded = InstructionAdded of Undefined
    // type InstructionFinished = InstructionFinished of Undefined

    // step 2: workflows
    // type AddRitual = AddRitual of Undefined
    // type AddInstruction = AddInstruction of Undefined
    // type FinishInstruction = FinishInstruction of Undefined

    // step 3: bounded context: Ritual domain

    // step 4: identify Domain Objects
    // type Ritual = Ritual of Undefined
    // type Instruction = Instruction of Undefined

    // step 5:
    // 1.1 AddRitual UnvalidatedRitual -> Ritual 
    // 2.1 AddInstruction Ritual UnvalidatedInstruction -> ActiveInstruction 
    // 3.1 FinishInstruction Ritual ActiveInstruction -> FinishedInstruction

    // step 6:
    // Ritual
    type UnvalidatedRitual = {
        UnvalidatedHeader : string
    }
    type RitualId = RitualId of int
    type InstructionId = InstructionId of int
    type String255 = String255 of string
    type RitualHeader = RitualHeader of string
    type Ritual = {
        Id : RitualId
        InstructionId : InstructionId
        Header : String255
    }

    // Instruction
    type UnvalidatedInstruction = {
        Header : string
    }
    type InstructionHeader = InstructionHeader of String255
    type ActiveInstruction = {
        Id: InstructionId
        Header : InstructionHeader
    }
    type FinishedInstruction = {
        Id: InstructionId
        Header : InstructionHeader
    }

    // step 7. refine workflow: identify workflow sub-steps
    // nothing

    // step 8. refine workflow: identify dependencies 
    // nothing

    // step 9. refine workflow: describe output events and side effects
    type RitualAdded = {
        Ritual : Ritual
    }
    type AddRitual = UnvalidatedRitual -> RitualAdded
    type InstructionAdded = {
        Ritual : Ritual
        Instruction : ActiveInstruction
    }
    type AddInstruction = Ritual -> UnvalidatedInstruction -> InstructionAdded

    type InstructionFinished = {
        Ritual : Ritual
        Instruction : FinishedInstruction
    }
    type AlreadyFinishedError = {
        Ritual : Ritual
        Instruction : FinishedInstruction
        ErrorDescription : string
    }
    type FinishInstruction = Ritual -> ActiveInstruction -> InstructionFinished
    type ReviveInstruction = Ritual -> FinishedInstruction -> InstructionAdded

    // *********************************
    // PART 2 Implementation
    // *********************************

    let addRitual : AddRitual =
        fun unvalidatedRitual ->
            let ritualId = RitualId 1
            let instructionId = InstructionId 1
            let header = String255 "test"
            // unvalidatedRitual.UnvalidatedHeader |> RitualHeader.create
            let validatedRitual : Ritual = {
                Id = ritualId 
                InstructionId = instructionId
                Header = header
            }
            let ritualAdded = {
              Ritual = validatedRitual
            }
            ritualAdded
            

// smart constructor using private
(* 
type OrderId = private OrderId of string
module OrderId =
  let create str =
    OrderId str
  let value (OrderId str) =
    str

module Common =
    let orderId = OrderId.create "Oliver"
    printfn "Print %s" (OrderId.value orderId)
*)